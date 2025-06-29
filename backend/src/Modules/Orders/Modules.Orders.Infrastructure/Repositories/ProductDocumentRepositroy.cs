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
    public async Task AddProductItem(Guid Id, ProductItemDocument productItemDocument)
    {
        var client = elasticClientFactory.CreateElasticClient();
        await client.UpdateAsync<ProductDocument>(Id.ToString(), x =>
            x.Index("products")
            .Script(x => x.Source(@"
                if (ctx._source.productItemDocuments == null ) {
                    ctx._source.productItemDocuments = [params.productItemDocument]
                } else { 
                    boolean exists = false;
                    for (item in ctx._source.productItemDocuments) {
                        if (item.id == params.productItemDocument.id) {
                            exists = true;
                            break;
                        }
                    }
                    if (!exists) {
                        ctx._source.productItemDocuments.add(params.productItemDocument);
                    }
                }
            ").Params(p => p.Add("productItemDocument", productItemDocument))));
    }
    public async Task Delete(Guid Id)
    {
        var client = elasticClientFactory.CreateElasticClient();
        await client.DeleteAsync<ProductDocument>(Id.ToString(), x =>
            x.Index("products"));
    }
    public async Task RemoveProductItem(Guid Id, Guid ProductItemId)
    {
        var client = elasticClientFactory.CreateElasticClient();
        await client.UpdateAsync<ProductDocument>(Id.ToString(), x =>
            x.Index("products")
            .Script(x => x.Source(@"
                if (ctx._source.productItemDocuments != null) {
                    def updatedList = [];
                    for (item in ctx._source.productItemDocuments) {
                        if (item.id != params.productItemId) {
                            updatedList.add(item);
                        }
                    }
                    ctx._source.productItemDocuments = updatedList;
                }
            ").Params(p => p.Add("productItemId", ProductItemId.ToString()))));
    }
}
