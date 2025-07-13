using Nest;

namespace Modules.Catalog.Application.Abstractions;

public interface IElasticClientFactory
{
    IElasticClient CreateElasticClient();
}