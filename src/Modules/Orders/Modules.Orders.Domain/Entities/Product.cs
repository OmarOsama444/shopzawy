using Modules.Common.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Domain.Entities;

public class Product : Entity
{
    public Guid Id { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public string LongDescription { get; private set; } = string.Empty;
    public string ShortDescription { get; private set; } = string.Empty;
    public bool InStock { get; private set; } = false;
    public DateTime CreatedOn { get; set; }
    public WeightUnit weightUnit { get; set; }
    public float weight { get; set; }
    public float Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public DimensionUnit dimensionUnit { get; set; }
    public float Width { get; set; }
    public float Length { get; set; }
    public float Height { get; set; }
    public ICollection<string> Tags { get; set; } = [];
    public Guid VendorId { get; private set; }
    public string BrandName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public virtual Vendor Vendor { get; set; } = default!;
    public virtual Brand Brand { get; private set; } = default!;
    public virtual Category Category { get; set; } = default!;
    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = [];
    public virtual ICollection<ProductDiscount> ProductDiscounts { get; set; } = [];
    public virtual ICollection<ProductItem> ProductItems { get; set; } = [];
    public static Product Create(
    string productName,
    string longDescription,
    string shortDescription,
    string imageUrl,
    WeightUnit weightUnit,
    float weight,
    float price,
    DimensionUnit dimensionUnit,
    float width,
    float length,
    float height,
    ICollection<string> tags,
    Guid vendorId,
    string brandName,
    string categoryName)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = productName,
            LongDescription = longDescription,
            ShortDescription = shortDescription,
            InStock = false,
            CreatedOn = DateTime.UtcNow,
            ImageUrl = imageUrl,
            weightUnit = weightUnit,
            weight = weight,
            Price = price,
            dimensionUnit = dimensionUnit,
            Width = width,
            Length = length,
            Height = height,
            Tags = tags,
            VendorId = vendorId,
            BrandName = brandName,
            CategoryName = categoryName
        };
        return product;
    }
}
