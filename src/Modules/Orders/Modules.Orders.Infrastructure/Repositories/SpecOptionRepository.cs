using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class SpecOptionRepository(OrdersDbContext ordersDbContext) : Repository<SpecificationOption, OrdersDbContext>(ordersDbContext), ISpecOptionRepository
{
}
