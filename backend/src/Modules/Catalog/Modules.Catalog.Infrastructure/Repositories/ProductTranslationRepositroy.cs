using Common.Domain.ValueObjects;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities.Translation;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

public class ProductTranslationRepositroy(OrdersDbContext dbContext) :
    Repository<ProductTranslation, OrdersDbContext>(dbContext), IProductTranslationsRepository
{
    public async Task<ProductTranslation?> GetByIdAndLang(Guid id, Language langCode)
    {
        return await context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == id && x.LangCode == langCode);

    }
}
