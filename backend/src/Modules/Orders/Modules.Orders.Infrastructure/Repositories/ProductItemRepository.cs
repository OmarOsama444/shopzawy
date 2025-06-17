using Common.Infrastructure;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories;

public class ProductItemRepository(
    OrdersDbContext ordersDbContext
    ) :
    Repository<ProductItem, OrdersDbContext>(ordersDbContext),
    IProductItemRepository
{
    public ProductItem? GetByProductIdAndSku(Guid productId, string sku)
    {
        return context
            .ProductItems
            .FirstOrDefault(
                x =>
                x.ProductId == productId
                &&
                x.StockKeepingUnit == sku
            );
    }

}
