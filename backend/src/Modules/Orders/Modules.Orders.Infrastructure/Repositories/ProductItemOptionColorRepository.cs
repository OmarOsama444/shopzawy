using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories;

public class ProductItemOptionColorRepository(OrdersDbContext context)
    : Repository<ProductItemOptionColor, OrdersDbContext>(context),
      IProductItemOptionColorRepository
{
    public async Task<ProductItemOptionColor?> GetByIdAndValueAndSpecId(Guid id, string value, Guid specId)
    {
        return await context.ProductItemOptionColors.FirstOrDefaultAsync(
            x => x.ProductItemId == id &&
                 x.ColorCode == value &&
                 x.SpecificationId == specId
        );
    }
}

