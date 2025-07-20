using Common.Application;
using Common.Application.InjectionLifeTime;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Infrastructure.Config;
using Modules.Catalog.Infrastructure.Data;
using Modules.Catalog.Infrastructure.OutBox;

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

        services.Scan(
            scan => scan
                .FromAssemblies(
                    Application.AssemblyRefrence.Assembly,
                    Infrastructure.AssemblyRefrence.Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IScopped)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );

        services.Scan(
            scan => scan
                .FromAssemblies(
                    Application.AssemblyRefrence.Assembly,
                    Infrastructure.AssemblyRefrence.Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(ISingleton)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime()
            );

        services.Scan(
             scan => scan
                 .FromAssemblies(
                     Application.AssemblyRefrence.Assembly,
                     Infrastructure.AssemblyRefrence.Assembly)
                 .AddClasses(classes => classes.AssignableTo(typeof(ITransistent)))
                 .AsImplementedInterfaces()
                 .WithTransientLifetime()
             );

        services.AddScoped<IDbConnectionFactory>(sp => new DbConnectionFactory(DbConnectionString));
        services.AddScoped<IOrdersDbContext, OrdersDbContext>();

        #region Elastic Options
        services.ConfigureOptions<ElasticOptionsSetup>();
        #endregion

        // decorates all the notification handlers in the application layer only
        services.Decorate(typeof(INotificationHandler<>)
            , (inner, serviceProvider) =>
            {
                var innerType = inner.GetType();
                var innerAssembly = innerType.Assembly;
                if (innerAssembly == Application.AssemblyRefrence.Assembly)
                {
                    var decoratorType = typeof(OutboxIdempotentDomainEventHandlerDecorator<>)
                        .MakeGenericType(innerType.GetInterfaces()[0].GenericTypeArguments);

                    return ActivatorUtilities.CreateInstance(serviceProvider, decoratorType, inner);
                }
                return inner;
            });

    }
}
