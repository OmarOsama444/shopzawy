using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

public class BannerRepository(OrdersDbContext ordersDbContext) : Repository<Banner, OrdersDbContext>(ordersDbContext), IBannerRepository
{
    public async Task<ICollection<Banner>> GetBannerByActive(bool isActive)
    {
        return await context.banners.Where(x => x.Active == isActive).ToListAsync();
    }

    public async Task<ICollection<BannerResponse>> Paginate(int pageNumber, int pageSize, string? Title, bool? isActive)
    {
        int offset = (pageNumber - 1) * pageSize;
        return await context.banners
            .Where(x => isActive == null || x.Active == isActive)
            .Where(x => Title == null || x.Title.StartsWith(Title))
            .Skip(offset)
            .Take(pageSize)
            .Select(x => new BannerResponse(
                x.Id,
                x.Title,
                x.Description,
                x.Link,
                x.Active,
                x.Position.ToString(),
                x.Size.ToString()))
            .ToListAsync();
    }
    public async Task<int> Total(string? Title, bool? isActive)
    {
        return await context.banners
            .Where(x => isActive == null || x.Active == isActive)
            .Where(x => Title == null || x.Title.StartsWith(Title))
            .CountAsync();
    }
}
