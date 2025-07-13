using Common.Domain.Entities;
using Modules.Catalog.Domain.DomainEvents;
using Modules.Catalog.Domain.Entities.Translation;
using Modules.Catalog.Domain.ValueObjects;

namespace Modules.Catalog.Domain.Entities;

public class Product : Entity
{
    public Guid Id { get; private set; }
    public List<string> Tags { get; set; } = [];
    public DateTime CreatedOn { get; set; }
    public WeightUnit WeightUnit { get; set; }
    public DimensionUnit DimensionUnit { get; set; }
    public Guid VendorId { get; private set; }
    public Guid BrandId { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Vendor Vendor { get; set; } = default!;
    public virtual Brand Brand { get; private set; } = default!;
    public virtual Category Category { get; set; } = default!;
    public virtual ICollection<ProductItem> ProductItems { get; set; } = [];
    public virtual ICollection<ProductTranslation> ProductTranslations { get; set; } = [];
    public static Product Create(
    WeightUnit weightUnit,
    DimensionUnit dimensionUnit,
    List<string> tags,
    Guid vendorId,
    Guid brandId,
    Guid categoryId)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            CreatedOn = DateTime.UtcNow,
            WeightUnit = weightUnit,
            DimensionUnit = dimensionUnit,
            Tags = tags,
            VendorId = vendorId,
            BrandId = brandId,
            CategoryId = categoryId
        };
        product.RaiseDomainEvent(new ProductCreatedDomainEvent(product.Id));
        return product;
    }

    public void Update(
        WeightUnit? weightUnit,
        DimensionUnit? dimensionUnit,
        List<string>? tags)
    {

        if (weightUnit is not null)
            this.WeightUnit = weightUnit.Value;

        if (dimensionUnit is not null)
            this.DimensionUnit = dimensionUnit.Value;

        if (tags is not null)
            Tags = tags;
    }

}
