using Microsoft.EntityFrameworkCore;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class ProductTranslationRepositroy(OrdersDbContext dbContext) :
    Repository<ProductTranslation, OrdersDbContext>(dbContext), IProductTranslationsRepository
{
    public async Task<ProductTranslation?> GetByIdAndLang(Guid id, Language langCode)
    {
        return await context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == id && x.LangCode == langCode);

    }
}
