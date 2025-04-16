using EFCore.NamingConventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Common.Infrastructure;
using Modules.Common.Infrastructure.interceptors;
using Modules.Common.Presentation.Endpoints;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Infrastructure;

public static class OrdersModule
{
    public static void AddOrdersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpoints(Modules.Orders.Presentation.AssemblyRefrence.Assembly);
        services.AddInfrastructure(configuration);
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string DbConnectionString = configuration.GetConnectionString("ShopizawyDb")!;
        // services.AddOptions<OutBoxOptions>()
        //     .BindConfiguration("Users:OutboxOptions")
        //     .ValidateDataAnnotations();

        // services.ConfigureOptions<ConfigureProcessOutboxJob>();

        // var GmailOptions = configuration.GetSection("Users:GmailConfig");

        services.AddDbContext<OrdersDbContext>((sp, options) =>
        {
            options.UseNpgsql(
                DbConnectionString,
                options
                    =>
                {
                    options
                        .MigrationsAssembly(Modules.Orders.Infrastructure.AssemblyRefrence.Assembly)
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Orders);
                }
            )
            .UseSnakeCaseNamingConvention()
            .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptors>());
        });

        services.TryAddSingleton<PublishDomainEventsInterceptors>();

        // registers all the repositorys in this assembly
        services.Scan(
            scan => scan
                .FromAssemblies(Modules.Orders.Infrastructure.AssemblyRefrence.Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDbConnectionFactory>(sp => new DbConnectionFactory(DbConnectionString));
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

    }
}
