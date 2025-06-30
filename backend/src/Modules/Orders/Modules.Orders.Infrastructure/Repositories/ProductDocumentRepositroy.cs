using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Elastic;

namespace Modules.Orders.Infrastructure.Repositories;

public class ProductDocumentRepositroy(IElasticClientFactory elasticClientFactory) : IProductDocumentRepository
{
    public async Task Add(ProductDocument productDocument)
    {
        var client = elasticClientFactory.CreateElasticClient();
        await client.IndexAsync<ProductDocument>(productDocument, x => x.Index("products").Id(productDocument.Id));
    }
    public async Task Delete(Guid Id)
    {
        var client = elasticClientFactory.CreateElasticClient();
        await client.DeleteAsync<ProductDocument>(Id.ToString(), x =>
            x.Index("products"));
    }

}
