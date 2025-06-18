using Common.Infrastructure;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities.Views;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories;

public class CategoryStatisticsRepository(OrdersDbContext ordersDbContext) : Repository<CategoryStatistics, OrdersDbContext>(ordersDbContext), ICategoryStatisticsRepository { }

