using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Application.Repositories;

public interface IBannerRepository : IRepository<Banner>
{
    Task<ICollection<Banner>> GetBannerByActive(bool isActive);
    Task<int> Total(string? Title, bool? isActive);
    Task<ICollection<BannerResponse>> Paginate(int pageNumber, int pageSize, string? Title, bool? isActive);
}

public record BannerResponse(
    Guid Id,
    string Title,
    string Description,
    string Link,
    bool Active,
    string Position,
    string Size
);