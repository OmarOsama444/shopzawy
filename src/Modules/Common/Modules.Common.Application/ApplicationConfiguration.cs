using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Modules.Common.Application.Behaviour;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Modules.Common.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies);
                cfg.AddOpenBehavior(typeof(RequestLoggingPiplineBehaviour<,>));
                cfg.AddOpenBehavior(typeof(ValidationPiplineBehaviour<,>));
                cfg.AddOpenBehavior(typeof(CachingPiplineBehaviour<,>));
            });
            services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);
            return services;
        }

    }
}
