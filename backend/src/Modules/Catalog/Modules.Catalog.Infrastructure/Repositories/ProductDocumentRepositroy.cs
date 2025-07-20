using Common.Application.InjectionLifeTime;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Elastic;

namespace Modules.Catalog.Infrastructure.Repositories;

public class ProductDocumentRepositroy(IElasticClientFactory elasticClientFactory) : IProductDocumentRepository, IScopped
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

    public async Task Update(ProductDocument productDocument)
    {
        var client = elasticClientFactory.CreateElasticClient();
        await client
            .UpdateAsync<ProductDocument>(
                productDocument.Id,
                x => x.Index("products")
                .Doc(productDocument));
    }
}
