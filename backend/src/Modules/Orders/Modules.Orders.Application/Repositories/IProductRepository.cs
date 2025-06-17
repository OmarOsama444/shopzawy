using Common.Domain;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Repositories;

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
    public Task<ICollection<Product>> GetByCategoryId(Guid categoryId);
}
