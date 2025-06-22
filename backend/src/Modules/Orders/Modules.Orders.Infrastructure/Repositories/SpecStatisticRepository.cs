using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities.Views;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories;

public class SpecStatisticRepository
    (OrdersDbContext ordersDbContext) :
    Repository<SpecificationStatistics, OrdersDbContext>(ordersDbContext), ISpecStatisticRepository
{
    public Task<SpecificationStatistics?> GetByIdAndValueAsync(Guid id, string value, CancellationToken cancellationToken = default)
    {
        return context.SpecificationStatistics
            .FirstOrDefaultAsync(x => x.Id == id && x.Value == value, cancellationToken);
    }

}
