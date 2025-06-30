using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories;

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
