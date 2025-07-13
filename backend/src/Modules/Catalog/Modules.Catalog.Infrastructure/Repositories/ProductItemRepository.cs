using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

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
