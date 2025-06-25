using Nest;

namespace Modules.Orders.Application.Abstractions;

public interface IElasticClientFactory
{
    IElasticClient CreateElasticClient();
}