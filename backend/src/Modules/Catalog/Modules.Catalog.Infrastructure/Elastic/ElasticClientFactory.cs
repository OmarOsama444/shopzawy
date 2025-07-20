using Common.Application.InjectionLifeTime;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Infrastructure.Config;
using Nest;

namespace Modules.Catalog.Infrastructure.Elastic;


public class ElasticClientFactory : IElasticClientFactory, ISingleton
{
    private readonly IElasticClient _client;

    public ElasticClientFactory(IOptions<ElasticOptions> options)
    {
        var uri = new Uri(options.Value.ConnectionString);
        var settings = new ConnectionSettings(uri)
            .DefaultIndex("products")
            .EnableDebugMode()
            .PrettyJson();

        _client = new ElasticClient(settings);
    }

    public IElasticClient CreateElasticClient() => _client;
}

