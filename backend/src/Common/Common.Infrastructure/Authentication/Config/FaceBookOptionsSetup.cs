using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
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
        options.SignInScheme = "External";
        options.Scope.Add("email");
        options.Scope.Add("public_profile");
        options.Fields.Add("first_name");
        options.Fields.Add("last_name");
        options.Fields.Add("email");

        options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "first_name");
        options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "last_name");

    }

    public void Configure(FacebookOptions options)
        => Configure(Options.DefaultName, options);
}
