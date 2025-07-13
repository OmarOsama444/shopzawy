using Common.Domain.ValueObjects;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities.Translation;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

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
