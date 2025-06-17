using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Authentication.Config;

public sealed class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    private string SectionName = "Jwt";
    public void Configure(JwtOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
    }
}
