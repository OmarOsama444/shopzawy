using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class CategorySpecRepository(OrdersDbContext ordersDbContext) :
    Repository<CategorySpec, OrdersDbContext>(ordersDbContext), ICategorySpecRepositroy
{
    public async Task UpdateCategoryName(string from, string to)
    {
        await context.CategorySpecs
        .Where(p => p.CategoryName == from)
        .ExecuteUpdateAsync(setters => setters
            .SetProperty(p => p.CategoryName, to));
    }

}
