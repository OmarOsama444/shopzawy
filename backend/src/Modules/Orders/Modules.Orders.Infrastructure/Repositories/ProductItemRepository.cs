using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
    public Task<ProductItem?> GetByProductIdAndSku(Guid productId, string sku)
    {
        return context
            .ProductItems
            .FirstOrDefaultAsync(
                x =>
                x.ProductId == productId
                &&
                x.StockKeepingUnit == sku
            );
    }

}
