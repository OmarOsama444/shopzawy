using Common.Application;
using Common.Domain;
using Common.Infrastructure.interceptors;
using Common.Presentation.Endpoints;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Application.Services;
using Modules.Orders.Infrastructure.Config;
using Modules.Orders.Infrastructure.Data;
using Modules.Orders.Infrastructure.Elastic;
using Modules.Orders.Infrastructure.OutBox;
using Modules.Orders.Infrastructure.Repositories;
using Modules.Orders.Infrastructure.Services;

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
        services.AddOptions<OutBoxOptions>()
            .BindConfiguration("Orders:OutboxOptions")
            .ValidateDataAnnotations();

        services.ConfigureOptions<ConfigureProcessOutboxJob>();


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
            .AddInterceptors(sp.GetRequiredService<PublishOutboxMessagesInterceptor>());
        });
        services.AddTransient<PublishOutboxMessagesInterceptor>();

        // registers all the repositorys in this assembly
        services.Scan(
            scan => scan
                .FromAssemblies(Modules.Orders.Infrastructure.AssemblyRefrence.Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDbConnectionFactory>(sp => new DbConnectionFactory(DbConnectionString));
        services.AddScoped<IElasticClientFactory, ElasticClientFactory>();
        services.AddScoped<IOrdersDbContext, OrdersDbContext>();
        services.AddScoped<IProductDocumentRepository, ProductDocumentRepositroy>();

        #region Elastic Options
        services.ConfigureOptions<ElasticOptionsSetup>();
        #endregion

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
