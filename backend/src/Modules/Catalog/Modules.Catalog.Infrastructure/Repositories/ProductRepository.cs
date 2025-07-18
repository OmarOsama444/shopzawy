using Common.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Infrastructure.Data;


namespace Modules.Catalog.Infrastructure.Repositories;

public class ProductRepository(OrdersDbContext ordersDbContext) : Repository<Product, OrdersDbContext>(ordersDbContext), IProductRepository
{
    public async Task<ICollection<Product>> GetByCategoryId(Guid categoryId)
    {
        return await context.Products.Where(x => x.Id == categoryId).ToListAsync();
    }
    public ICollection<Product> Paginate(int pageNumber, int pageSize, ICollection<Guid> categoryIds, bool? OnSale, KeyValuePair<int, int>? PriceRange, KeyValuePair<DateTime, DateTime> DateRange)
    {
        throw new NotImplementedException();
    }

    public Task<int> VendorProductsCount(Guid vendorId)
    {
        throw new NotImplementedException();
    }
}
