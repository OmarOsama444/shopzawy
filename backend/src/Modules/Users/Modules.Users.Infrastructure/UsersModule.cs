using Common.Application;
using Common.Infrastructure.interceptors;
using Common.Presentation.Endpoints;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Users.Application;
using Modules.Users.Application.Abstractions;
using Modules.Users.Application.Services;
using Modules.Users.Infrastructure.Authentication;
using Modules.Users.Infrastructure.Data;
using Modules.Users.Infrastructure.Options;
using Modules.Users.Infrastructure.OutBox;
using Modules.Users.Infrastructure.Services;

namespace Modules.Users.Infrastructure;

public static class UsersModule
{
    public static void AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyRefrence.Assembly);
        services.AddInfrastructure(configuration);
    }
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string DbConnectionString = configuration.GetConnectionString("ShopizawyDb")!;
        services.AddOptions<GmailSmtpOptions>()
            .BindConfiguration("Users:GmailConfig")
            .ValidateDataAnnotations();
        services.AddOptions<OutBoxOptions>()
            .BindConfiguration("Users:OutboxOptions")
            .ValidateDataAnnotations();

        var GmailOptions = configuration.GetSection("Users:GmailConfig");

        services
            .AddFluentEmail(GmailOptions["SenderEmail"])
            .AddSmtpSender(
                GmailOptions["SmtpServer"],
                GmailOptions.GetValue<int>("Port"),
                GmailOptions["SenderEmail"],
                GmailOptions["SenderPassword"]);

        services.AddDbContext<UsersDbContext>((sp, options) =>
        {
            options.UseNpgsql(
                DbConnectionString,
                options
                    => options.MigrationsAssembly(
                        Infrastructure.AssemblyRefrence.Assembly)
            )
            .UseSnakeCaseNamingConvention();
            options
            .AddInterceptors(
                sp.GetRequiredService<PublishDomainEventsInterceptors>())
            .EnableSensitiveDataLogging();
        });

        services.TryAddSingleton<PublishDomainEventsInterceptors>();


        services.Scan(
            scan => scan
                .FromAssemblies(Infrastructure.AssemblyRefrence.Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDbConnectionFactory>(sp => new DbConnectionFactory(DbConnectionString));
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUserService, UserService>();
    }
}

// the out box job configuration

// services.ConfigureOptions<ConfigureProcessOutboxJob>();
// the publish outbox message interceptor configurations

// services.TryAddSingleton<PublishOutboxMessagesInterceptor>();

// registers all the repositorys in this assembly
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


