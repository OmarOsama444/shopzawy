using Common.Domain.ValueObjects;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities.Translation;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

public class BrandTranslationRepositorty(OrdersDbContext ordersDbContext) : Repository<BrandTranslation, OrdersDbContext>(ordersDbContext), IBrandTranslationRepository
{
    public async Task<BrandTranslation?> GetByIdAndLang(Guid id, Language langCode)
    {
        return
            await
                context
                    .BrandTranslations
                    .FirstOrDefaultAsync
                        (
                            ct => ct.BrandId == id &&
                            ct.LangCode == langCode
                        );
    }
}
