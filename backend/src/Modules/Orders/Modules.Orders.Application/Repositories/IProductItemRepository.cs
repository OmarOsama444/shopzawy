using Common.Domain;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Repositories;

public interface IProductItemRepository : IRepository<ProductItem>
{
    public Task<ProductItem?> GetByProductIdAndSku(Guid productId, string sku);
}
