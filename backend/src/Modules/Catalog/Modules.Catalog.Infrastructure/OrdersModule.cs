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
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Application.Services;
using Modules.Catalog.Infrastructure.Config;
using Modules.Catalog.Infrastructure.Data;
using Modules.Catalog.Infrastructure.Elastic;
using Modules.Catalog.Infrastructure.OutBox;
using Modules.Catalog.Infrastructure.Repositories;
using Modules.Catalog.Infrastructure.Services;

namespace Modules.Catalog.Infrastructure;

public static class CatalogModule
{
    public static void AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpoints(Presentation.AssemblyRefrence.Assembly);
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
                        .MigrationsAssembly(AssemblyRefrence.Assembly)
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Catalog);
                }
            )
            .UseSnakeCaseNamingConvention()
            .AddInterceptors(sp.GetRequiredService<PublishOutboxMessagesInterceptor>());
        });
        services.AddTransient<PublishOutboxMessagesInterceptor>();

        // registers all the repositorys in this assembly
        services.Scan(
            scan => scan
                .FromAssemblies(AssemblyRefrence.Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDbConnectionFactory>(sp => new DbConnectionFactory(DbConnectionString));
        services.AddSingleton<IElasticClientFactory, ElasticClientFactory>();
        services.AddSingleton<IElasticSearchQueryService, ElasticSearchQueryService>();
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
