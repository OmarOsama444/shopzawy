using System.IdentityModel.Tokens.Jwt;
using Common.Infrastructure.Authentication.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure.Authentication;

internal static class AuthenticationExtensions
{
    internal static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddAuthentication()
        .AddJwtBearer()
        .AddCookie("External")
        .AddGoogle()
        .AddFacebook();

        services.AddHttpContextAccessor();
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.ConfigureOptions<AuthenticationOptionsSetup>();
        services.ConfigureOptions<GoogleOptionsSetup>();
        services.ConfigureOptions<FaceBookOptionsSetup>();
        return services;
    }
}
