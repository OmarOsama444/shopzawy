using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Authentication.Config;

public class GoogleOptionsSetup : IConfigureNamedOptions<GoogleOptions>
{
    private readonly IConfiguration _config;

    public GoogleOptionsSetup(IConfiguration config)
    {
        _config = config;
    }

    public void Configure(string? name, GoogleOptions options)
    {
        if (name != GoogleDefaults.AuthenticationScheme) return;

        options.ClientId = _config["Authentication:Google:ClientId"]!;
        options.ClientSecret = _config["Authentication:Google:ClientSecret"]!;
        options.SignInScheme = "External";
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
        options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");

    }

    public void Configure(GoogleOptions options)
        => Configure(Options.DefaultName, options);
}
