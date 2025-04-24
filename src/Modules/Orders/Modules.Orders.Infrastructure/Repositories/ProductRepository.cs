using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class ProductRepository(OrdersDbContext ordersDbContext) : Repository<Product, OrdersDbContext>(ordersDbContext), IProductRepository
{
    public override Task<Product?> GetByIdAsync(object id)
    {
        Guid Id = (Guid)id;
        return context.Products
            .Include(x => x.Category)
            .ThenInclude(x => x.CategorySpecs)
            .ThenInclude(x => x.Specification)
            .Include(x => x.ProductItems)
            .FirstOrDefaultAsync(x => x.Id == Id);
    }
    public async Task<ICollection<Product>> GetByCategoryName(string categoryName)
    {
        return await context.Products.Where(x => x.CategoryName == categoryName).ToListAsync();
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
