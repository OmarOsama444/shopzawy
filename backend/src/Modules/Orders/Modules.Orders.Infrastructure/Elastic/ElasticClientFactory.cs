using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Infrastructure.Config;
using Nest;

namespace Modules.Orders.Infrastructure.Elastic
{

    public class ElasticClientFactory(IOptions<ElasticOptions> options) : IElasticClientFactory
    {
        public IElasticClient CreateElasticClient()
        {
            return new ElasticClient(new Uri(options.Value.ConnectionString));
        }
    }

}

