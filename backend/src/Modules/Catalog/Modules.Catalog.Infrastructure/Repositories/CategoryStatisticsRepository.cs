using Common.Infrastructure;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities.Views;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

public class CategoryStatisticsRepository(OrdersDbContext ordersDbContext) : Repository<CategoryStatistics, OrdersDbContext>(ordersDbContext), ICategoryStatisticsRepository { }

