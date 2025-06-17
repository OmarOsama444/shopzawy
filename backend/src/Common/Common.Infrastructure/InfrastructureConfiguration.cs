using Quartz;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Common.Application.Caching;
using Common.Application.Clock;
using Common.Application.EventBus;
using Common.Infrastructure.Authentication;
using Common.Infrastructure.Authorization;
using Common.Infrastructure.Caching.DistributedCache;
using Common.Infrastructure.Clock;
namespace Common.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructure(
            this IServiceCollection services,
            Action<IRegistrationConfigurator>[] moduleConfigureConsumers
            , IConfiguration config)
        {
            #region redis cache Server setup
            //IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect( redisConnectionString );
            //services.AddStackExchangeRedisCache( 
            //    options => 
            //    options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer) );
            //services.TryAddSingleton<ICacheService, DistributedCacheService>();
            #endregion

            #region memory cache
            services.AddDistributedMemoryCache();
            services.TryAddSingleton<ICacheService, CacheService>();
            #endregion

            services.AddAuthenticationInternal();
            services.AddAuthorizationInternal();
            // Interceptor That Publishes Domain Events
            // services.TryAddSingleton<PublishDomainEventsInterceptors>();

            // adding DateTime Providers use diffirent datetime provider when unitTesting 
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            // // adding auth provider 
            // services.AddSingleton<IAuthorizationPolicyProvider, RoleAuthorizationPolicyProvider>();
            // services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
            // Event bus interface for massTransit can be linked to RabbitMq when needed
            services.TryAddSingleton<IEventBus, EventBus>();
            services.AddMassTransit(config =>
            {
                foreach (var consumer in moduleConfigureConsumers)
                    consumer(config);
                config.SetKebabCaseEndpointNameFormatter();
                config.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            // adding quartz for background jobs 
            services.AddQuartz();
            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        }
    }
}
