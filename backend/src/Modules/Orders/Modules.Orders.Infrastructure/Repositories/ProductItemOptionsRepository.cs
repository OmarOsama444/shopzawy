using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories;

public class ProductItemOptionsRepository(OrdersDbContext ordersDbContext) : Repository<ProductItemOptions, OrdersDbContext>(ordersDbContext), IProductItemOptionsRepository
{
    public async Task<ProductItemOptions?> GetByIdAndValueAndSpecId(Guid Id, string Value, Guid SpecId)
    {
        return await context.ProductItemOptions.FirstOrDefaultAsync(x => x.Id == Id && x.Value.ToLower() == Value.ToLower() && x.SpecificationId == SpecId);
    }

}
