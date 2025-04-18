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
    public async Task<ICollection<SpecResponse>> Paginate(int pageNumber, int pageSize, string? name)
    {
        return await context.Specifications
            .Where(x => name == null || x.Name.StartsWith(name))
            .Skip((pageNumber - 1) * pageSize)
            .Include(x => x.SpecificationOptions)
            .Take(pageSize)
            .Select(x => new SpecResponse(x.Id, x.Name, x.DataType,
                x.SpecificationOptions.Select(c => new SpecOptionsResponse(c.Id, c.Value)).ToList()
            ))
            .ToListAsync();
    }
    public async Task<int> Total(string? name)
    {
        return await context.Specifications
            .Where(x => name == null || x.Name.StartsWith(name)).CountAsync();
    }

}