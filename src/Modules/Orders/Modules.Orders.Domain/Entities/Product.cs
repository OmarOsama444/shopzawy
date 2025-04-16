using Modules.Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class Product : Entity
{
    public Guid Id { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public string LDescription { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public float Price { get; private set; }
    public Guid VendorId { get; private set; }
    public string BrandName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public virtual Vendor Vendor { get; set; } = default!;
    public virtual Brand Brand { get; private set; } = default!;
    public virtual Category MainCategory { get; set; } = default!;
    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = [];
    public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; } = [];
    public virtual ICollection<ProductItem> ProductItems { get; set; } = [];
}
