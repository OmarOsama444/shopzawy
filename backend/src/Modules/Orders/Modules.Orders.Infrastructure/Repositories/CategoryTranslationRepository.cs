using Common.Domain.ValueObjects;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories;

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
