using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class ProductRepository(OrdersDbContext ordersDbContext) : Repository<Product, OrdersDbContext>(ordersDbContext), IProductRepository
{
    public ICollection<Product> Paginate(int pageNumber, int pageSize, ICollection<Guid> categoryIds, bool? OnSale, KeyValuePair<int, int>? PriceRange, KeyValuePair<DateTime, DateTime> DateRange)
    {
        throw new NotImplementedException();
    }

    public async Task<int> VendorProductsCount(Guid vendorId)
    {
        // TODO Add logic here
        return 0;
    }
}
