using Common.Infrastructure;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories;

public class ProductItemOptionsRepository(OrdersDbContext ordersDbContext) : Repository<ProductItemOptions, OrdersDbContext>(ordersDbContext), IProductItemOptionsRepository
{
}
