using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    public ICollection<Product> Paginate(
        int pageNumber,
        int pageSize,
        ICollection<Guid> categoryIds,
        bool? OnSale,
        KeyValuePair<int, int>? PriceRange,
        KeyValuePair<DateTime, DateTime> DateRange);

    public Task<int> VendorProductsCount(Guid vendorId);
    public Task<ICollection<Product>> GetByCategoryName(string categoryName);
}
