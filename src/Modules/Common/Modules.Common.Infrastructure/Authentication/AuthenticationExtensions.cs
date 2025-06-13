using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Modules.Common.Infrastructure.Authentication.Config;

namespace Modules.Common.Infrastructure.Authentication;

internal static class AuthenticationExtensions
{
    internal static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddAuthentication().AddJwtBearer();

        // .AddCookie("External")
        // .AddGoogle(googleOptions =>
        // {
        //     googleOptions.ClientId = config["Authentication:Google:ClientId"]!;
        //     googleOptions.ClientSecret = config["Authentication:Google:ClientSecret"]!;
        //     googleOptions.SignInScheme = "External";
        // })
        // .AddFacebook(facebookOptions =>
        // {
        //     facebookOptions.AppId = config["Authentication:Facebook:AppId"]!;
        //     facebookOptions.AppSecret = config["Authentication:Facebook:AppSecret"]!;
        //     facebookOptions.SignInScheme = "External";
        // });
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddHttpContextAccessor();

        services.ConfigureOptions<JwtBearerPostConfigureOptions>();

        return services;
    }
}
