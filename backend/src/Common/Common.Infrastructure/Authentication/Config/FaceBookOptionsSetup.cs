using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Common.Infrastructure.Authentication.Config;

public class FaceBookOptionsSetup : IConfigureNamedOptions<FacebookOptions>
{
    private readonly IConfiguration _config;

    public FaceBookOptionsSetup(IConfiguration config)
    {
        _config = config;
    }

    public void Configure(string? name, FacebookOptions options)
    {
        if (name != FacebookDefaults.AuthenticationScheme) return;

        options.AppId = _config["Authentication:Facebook:AppId"]!;
        options.AppSecret = _config["Authentication:Facebook:AppSecret"]!;
        options.SignInScheme = "External"; // or your configured scheme
    }

    public void Configure(FacebookOptions options)
        => Configure(Options.DefaultName, options);
}
