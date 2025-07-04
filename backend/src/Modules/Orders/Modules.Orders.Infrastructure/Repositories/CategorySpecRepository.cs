using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories;

public class CategorySpecRepository(OrdersDbContext ordersDbContext) :
    Repository<CategorySpec, OrdersDbContext>(ordersDbContext), ICategorySpecRepositroy
{
    public async Task<CategorySpec?> GetByCategoryIdAndSpecId(Guid categoryId, Guid specId)
    {
        return await context.CategorySpecs.FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.SpecId == specId);
    }
}
