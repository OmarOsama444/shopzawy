using Modules.Common.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Domain.Entities;

public class Product : Entity
{
    public Guid Id { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public string LongDescription { get; private set; } = string.Empty;
    public string ShortDescription { get; private set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public WeightUnit weightUnit { get; set; }
    public float weight { get; set; }
    public DimensionUnit dimensionUnit { get; set; }
    public float Width { get; set; }
    public float Length { get; set; }
    public float Height { get; set; }
    public float Price { get; set; }
    public ICollection<string> Tags { get; set; } = [];
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
    float weight,
    DimensionUnit dimensionUnit,
    float width,
    float length,
    float height,
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
            weight = weight,
            dimensionUnit = dimensionUnit,
            Width = width,
            Length = length,
            Height = height,
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
    float? weight,
    DimensionUnit? dimensionUnit,
    float? width,
    float? length,
    float? height,
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

        if (weight is not null)
            this.weight = weight.Value;

        if (dimensionUnit is not null)
            this.dimensionUnit = dimensionUnit.Value;

        if (width is not null)
            Width = width.Value;

        if (length is not null)
            Length = length.Value;

        if (height is not null)
            Height = height.Value;

        if (tags is not null)
            Tags = tags;
    }

}
