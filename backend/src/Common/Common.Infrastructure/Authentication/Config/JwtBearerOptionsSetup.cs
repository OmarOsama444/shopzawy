using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Common.Infrastructure.Authentication.Config;

public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly IOptions<JwtOptions> _jwtOptions;

    public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name != JwtBearerDefaults.AuthenticationScheme)
            return;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = _jwtOptions.Value.Issuer,
            ValidAudience = _jwtOptions.Value.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey)
            )
        };
    }

    public void Configure(JwtBearerOptions options)
        => Configure(Options.DefaultName, options);
}


