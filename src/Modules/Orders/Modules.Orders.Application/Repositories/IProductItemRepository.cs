using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Application.Repositories;

public interface IProductItemRepository : IRepository<ProductItem>
{
    public ProductItem? GetByProductIdAndSku(Guid productId, string sku);
}
