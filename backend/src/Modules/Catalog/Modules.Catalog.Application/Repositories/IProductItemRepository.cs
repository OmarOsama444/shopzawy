using Common.Domain;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Application.Repositories;

public interface IProductItemRepository : IRepository<ProductItem>
{
    public Task<ProductItem?> GetByProductIdAndSku(Guid productId, string sku);
}
