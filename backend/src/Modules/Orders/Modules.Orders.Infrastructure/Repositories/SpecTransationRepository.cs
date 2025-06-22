using Common.Domain.ValueObjects;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
    public Task<SpecificationTranslation?> GetBySpecIdAndLanguage(Guid specId, Language language)
    {
        return context.SpecificationTranslations
            .FirstOrDefaultAsync(x => x.SpecId == specId && x.LangCode == language);
    }
}
