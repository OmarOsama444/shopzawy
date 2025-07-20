using Common.Application.Behaviour;
using Dapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Common.Application
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

            // dapper global sql mappers 
            // SqlMapper.AddTypeHandler(new ListSqlMapperHandler<Guid>());
            return services;
        }

    }
}
