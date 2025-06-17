using Common.Infrastructure;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories;

public class SpecTransationRepository(OrdersDbContext dbContext) :
    Repository<
        SpecificationTranslation,
        OrdersDbContext
    >(dbContext)
    , ISpecTranslationRepository
{
}
