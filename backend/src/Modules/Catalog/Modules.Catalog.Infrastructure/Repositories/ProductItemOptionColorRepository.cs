using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

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

