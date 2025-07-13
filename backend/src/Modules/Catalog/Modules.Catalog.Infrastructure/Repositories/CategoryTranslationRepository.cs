using Common.Domain.ValueObjects;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities.Translation;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

public class CategoryTranslationRepository(OrdersDbContext ordersDbContext)
    : Repository<CategoryTranslation, OrdersDbContext>(ordersDbContext),
    ICategoryTranslationRepository
{
    public async Task<CategoryTranslation?> GetByIdAndLang(Guid id, Language langCode)
    {
        return
            await
                context
                    .CategoryTranslations
                    .FirstOrDefaultAsync
                        (
                            ct => ct.CategoryId == id &&
                            ct.LangCode == langCode
                        );
    }
}
