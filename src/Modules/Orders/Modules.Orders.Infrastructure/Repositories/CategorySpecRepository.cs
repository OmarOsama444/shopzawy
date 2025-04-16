using Microsoft.EntityFrameworkCore;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class CategorySpecRepository(OrdersDbContext ordersDbContext) : Repository<CategorySpec, OrdersDbContext>(ordersDbContext), ICategorySpecRepository
{
    public async Task<CategorySpec?> GetByCategoryNameAndSpecId(string categoryName, Guid specId)
    {
        return await ordersDbContext.CategorySpecs.FirstOrDefaultAsync(c => c.CategoryName == categoryName && c.SpecId == specId);
    }

}
