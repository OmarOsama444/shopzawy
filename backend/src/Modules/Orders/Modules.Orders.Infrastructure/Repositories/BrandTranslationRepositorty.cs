using Common.Domain.ValueObjects;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories;

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
