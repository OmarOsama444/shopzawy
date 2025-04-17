using Microsoft.EntityFrameworkCore;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class SpecRepository(OrdersDbContext ordersDbContext) :
    Repository<Specification, OrdersDbContext>(ordersDbContext),
    ISpecRepository
{
    public async Task<ICollection<Specification>> GetByDataType(string dataTypeName)
    {
        return await context.Specifications.Where(s => s.DataType == dataTypeName).ToListAsync();
    }


    public async Task<Specification?> GetByNameAndCategoryName(string name, string categoryName)
    {
        var spec = await context.Specifications
        .Include(s => s.CategorySpecs)
        .FirstOrDefaultAsync(x => x.Name == name);

        if (spec?.CategorySpecs.Any(cs => cs.CategoryName == categoryName) == true)
        {
            return spec;
        }

        return null;
    }
}