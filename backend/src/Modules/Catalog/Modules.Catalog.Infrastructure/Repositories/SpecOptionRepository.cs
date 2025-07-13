using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

public class SpecOptionRepository : Repository<SpecificationOption, OrdersDbContext>, ISpecOptionRepository
{
    public SpecOptionRepository(OrdersDbContext ordersDbContext) : base(ordersDbContext) { }
    public async Task<ICollection<SpecificationOption>> GetBySpecId(Guid id)
    {
        return await context.SpecificationOptions.Where(x => x.SpecificationId == id).ToListAsync();
    }

    public async Task<SpecificationOption?> GetBySpecIdAndValue(Guid id, string value)
    {
        return await context.SpecificationOptions.FirstOrDefaultAsync(x => x.SpecificationId == id && x.Value == value);
    }
}
