using Modules.Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class Product : Entity
{
    public Guid Id { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public string LongDescription { get; private set; } = string.Empty;
    public string ShortDescription { get; private set; } = string.Empty;
    public bool InStock { get; private set; } = false;
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
