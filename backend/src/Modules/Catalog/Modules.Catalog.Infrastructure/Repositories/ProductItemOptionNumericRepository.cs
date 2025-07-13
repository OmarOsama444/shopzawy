using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

public class ProductItemOptionNumericRepository(OrdersDbContext context)
    : Repository<ProductItemOptionNumeric, OrdersDbContext>(context),
      IProductItemOptionNumericRepository
{
    public async Task<ProductItemOptionNumeric?> GetByIdAndValueAndSpecId(Guid id, float value, Guid specId)
    {
        return await context.ProductItemOptionNumerics.FirstOrDefaultAsync(
            x => x.ProductItemId == id &&
                 x.Value == value &&
                 x.SpecificationId == specId
        );
    }
}
