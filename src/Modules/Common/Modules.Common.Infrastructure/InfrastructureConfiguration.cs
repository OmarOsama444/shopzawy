using Quartz;
using MassTransit;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Modules.Common.Application.Clock;
using Modules.Common.Application.Caching;
using Modules.Common.Application.EventBus;
using Modules.Common.Infrastructure.Clock;
using Modules.Common.Infrastructure.Authentication;
using Modules.Common.Infrastructure.Caching.DistributedCache;
namespace Modules.Common.Infrastructure
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
