using Microsoft.EntityFrameworkCore;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class SpecOptionRepository : Repository<SpecificationOption, OrdersDbContext>, ISpecOptionRepository
{
    public SpecOptionRepository(OrdersDbContext ordersDbContext) : base(ordersDbContext) { }
    public async Task<ICollection<SpecificationOption>> GetBySpecId(Guid id)
    {
        return await context.SpecificationOptions.Where(x => x.SpecificationId == id).ToListAsync();
    }

}
