using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

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
