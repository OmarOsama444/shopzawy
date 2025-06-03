using Google.Protobuf.WellKnownTypes;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Common.Infrastructure.interceptors;
using Modules.Common.Presentation.Endpoints;
using Modules.Users.Application;
using Modules.Users.Application.Abstractions;
using Modules.Users.Application.Services;
using Modules.Users.Domain;
using Modules.Users.Domain.Repositories;
using Modules.Users.Infrastructure.Authentication;
using Modules.Users.Infrastructure.Data;
using Modules.Users.Infrastructure.Options;
using Modules.Users.Infrastructure.OutBox;
using Modules.Users.Infrastructure.Repositories;
using Modules.Users.Infrastructure.Services;
using Qdrant.Client;

namespace Modules.Users.Infrastructure;

public static class UsersModule
{
    public const string SchemaName = "USERS";
    public static void AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyRefrence.Assembly);
        services.AddInfrastructure(configuration);
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string DbConnectionString = configuration.GetConnectionString("ShopizawyDb")!;
        string FaceNetApi = configuration["Users:ConnectionStrings:FaceNetApi"]!;

        services.AddOptions<GmailSmtpOptions>()
            .BindConfiguration("Users:GmailConfig")
            .ValidateDataAnnotations();

        services.AddOptions<OutBoxOptions>()
            .BindConfiguration("Users:OutboxOptions")
            .ValidateDataAnnotations();

        // the out box job configuration

        // services.ConfigureOptions<ConfigureProcessOutboxJob>();

        var GmailOptions = configuration.GetSection("Users:GmailConfig");

        services
            .AddFluentEmail(GmailOptions["SenderEmail"])
            .AddSmtpSender(
                GmailOptions["SmtpServer"],
                GmailOptions.GetValue<int>("Port"),
                GmailOptions["SenderEmail"],
                GmailOptions["SenderPassword"]);


        services.AddDbContext<UserDbContext>((sp, options) =>
        {
            options.UseSqlServer(
                DbConnectionString,
                options
                    => options.MigrationsAssembly(
                        Infrastructure.AssemblyRefrence.Assembly)
            );
            options.AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptors>());
        });

        services.AddIdentity<User, IdentityRole<Guid>>(Options =>
        {
            Options.Password.RequireDigit = true;
            Options.Password.RequiredLength = 12;
            Options.Password.RequireLowercase = true;
            Options.Password.RequireUppercase = true;
            Options.Password.RequireNonAlphanumeric = true;
        })
        .AddEntityFrameworkStores<UserDbContext>();

        // the publish outbox message interceptor configurations

        // services.TryAddSingleton<PublishOutboxMessagesInterceptor>();

        // registers all the repositorys in this assembly
        services.Scan(
            scan => scan
                .FromAssemblies(Infrastructure.AssemblyRefrence.Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

        // the idempotent outbox domain event handler decorations


        // decorates all the notification handlers in the application layer only
        // services.Decorate(typeof(INotificationHandler<>)
        //     , (inner, serviceProvider) =>
        //     {
        //         var innerType = inner.GetType();
        //         var innerAssembly = innerType.Assembly;
        //         if (innerAssembly == Application.AssemblyRefrence.Assembly)
        //         {
        //             var decoratorType = typeof(OutboxIdempotentDomainEventHandlerDecorator<>)
        //                 .MakeGenericType(innerType.GetInterfaces()[0].GenericTypeArguments);

        //             return ActivatorUtilities.CreateInstance(serviceProvider, decoratorType, inner);
        //         }
        //         return inner;
        //     });

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IDbConnectionFactory>(sp => new DbConnectionFactory(DbConnectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFaceModelService>(sp => new FaceModelService(FaceNetApi));
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUserTokenRepository, UserTokenRepository>();

    }
}
