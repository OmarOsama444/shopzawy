using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Infrastructure.Config;
using Nest;

namespace Modules.Orders.Infrastructure.Elastic
{

    public class ElasticClientFactory : IElasticClientFactory
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

}

