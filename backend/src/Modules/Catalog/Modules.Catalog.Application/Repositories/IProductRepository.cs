using Common.Application;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Application.Repositories;

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
