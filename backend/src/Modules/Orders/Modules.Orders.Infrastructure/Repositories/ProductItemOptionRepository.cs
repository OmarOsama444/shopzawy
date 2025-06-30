using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories
{
    public class ProductItemOptionRepository(OrdersDbContext ordersDbContext) : Repository<ProductItemOptions, OrdersDbContext>(ordersDbContext), IProductItemOptionsRepository
    {
        public async Task<ProductItemOptions?> GetByIdAndValueAndSpecId(Guid Id, string Value, Guid SpecId)
        {
            return await context
                .ProductItemOptions
                .FirstOrDefaultAsync(x => x.ProductItemId == Id && x.Value == Value && x.SpecificationId == SpecId);
        }

    }

}

