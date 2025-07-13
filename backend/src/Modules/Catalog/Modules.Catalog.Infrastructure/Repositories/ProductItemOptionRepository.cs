using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

public class ProductItemOptionRepository(OrdersDbContext ordersDbContext) : Repository<ProductItemOptions, OrdersDbContext>(ordersDbContext), IProductItemOptionsRepository
{
    public async Task<ProductItemOptions?> GetByIdAndValueAndSpecId(Guid Id, string Value, Guid SpecId)
    {
        return await context
            .ProductItemOptions
            .FirstOrDefaultAsync(x => x.ProductItemId == Id && x.Value == Value && x.SpecificationId == SpecId);
    }

}

