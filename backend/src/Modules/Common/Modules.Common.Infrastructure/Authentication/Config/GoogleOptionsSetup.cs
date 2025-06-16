using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Modules.Common.Infrastructure.Authentication.Config;

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
    }

    public void Configure(GoogleOptions options)
        => Configure(Options.DefaultName, options);
}
