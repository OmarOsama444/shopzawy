using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Domain.Repositories;

public interface IProductItemRepository : IRepository<ProductItem>
{
    public ProductItem? GetByProductIdAndSku(Guid productId, string sku);
}
