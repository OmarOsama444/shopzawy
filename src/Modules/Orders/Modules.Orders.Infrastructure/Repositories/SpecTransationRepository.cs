using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Application.Abstractions;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class SpecTransationRepository(OrdersDbContext dbContext) :
    Repository<
        SpecificationTranslation,
        OrdersDbContext
    >(dbContext)
    , ISpecTranslationRepository
{
}
