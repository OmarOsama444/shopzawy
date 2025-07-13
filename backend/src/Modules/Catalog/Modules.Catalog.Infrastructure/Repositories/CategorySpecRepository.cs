using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

public class CategorySpecRepository(OrdersDbContext ordersDbContext) :
    Repository<CategorySpec, OrdersDbContext>(ordersDbContext), ICategorySpecRepositroy
{
    public async Task<CategorySpec?> GetByCategoryIdAndSpecId(Guid categoryId, Guid specId)
    {
        return await context.CategorySpecs.FirstOrDefaultAsync(x => x.CategoryId == categoryId && x.SpecId == specId);
    }
}
