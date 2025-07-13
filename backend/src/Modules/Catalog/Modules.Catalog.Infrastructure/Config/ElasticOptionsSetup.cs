using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Modules.Catalog.Infrastructure.Config;

public class ElasticOptionsSetup(IConfiguration configuration) : IConfigureOptions<ElasticOptions>
{
    public void Configure(ElasticOptions options)
    {
        options.ConnectionString = configuration["Orders:ElasticClientOptions:ConnectionString"] ?? string.Empty;
    }

}
