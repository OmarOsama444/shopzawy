using Modules.Common.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Domain.Entities;

public class Product : Entity
{
    public Guid Id { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public string LongDescription { get; private set; } = string.Empty;
    public string ShortDescription { get; private set; } = string.Empty;
    public ICollection<string> Tags { get; set; } = [];
    public DateTime CreatedOn { get; set; }
    public WeightUnit weightUnit { get; set; }
    public DimensionUnit dimensionUnit { get; set; }
    public Guid VendorId { get; private set; }
    public string BrandName { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public virtual Vendor Vendor { get; set; } = default!;
    public virtual Brand Brand { get; private set; } = default!;
    public virtual Category Category { get; set; } = default!;
    public virtual ICollection<ProductItem> ProductItems { get; set; } = [];
    public static Product Create(
    string productName,
    string longDescription,
    string shortDescription,
    WeightUnit weightUnit,
    DimensionUnit dimensionUnit,
    ICollection<string> tags,
    Guid vendorId,
    string brandName,
    Guid categoryId)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            ProductName = productName,
            LongDescription = longDescription,
            ShortDescription = shortDescription,
            CreatedOn = DateTime.UtcNow,
            weightUnit = weightUnit,
            dimensionUnit = dimensionUnit,
            Tags = tags,
            VendorId = vendorId,
            BrandName = brandName,
            CategoryId = categoryId
        };
        return product;
    }

    public void Update(
    string? productName,
    string? longDescription,
    string? shortDescription,
    WeightUnit? weightUnit,
    DimensionUnit? dimensionUnit,
    ICollection<string>? tags)
    {
        if (productName is not null)
            ProductName = productName;

        if (longDescription is not null)
            LongDescription = longDescription;

        if (shortDescription is not null)
            ShortDescription = shortDescription;

        if (weightUnit is not null)
            this.weightUnit = weightUnit.Value;

        if (dimensionUnit is not null)
            this.dimensionUnit = dimensionUnit.Value;

        if (tags is not null)
            Tags = tags;
    }

}
