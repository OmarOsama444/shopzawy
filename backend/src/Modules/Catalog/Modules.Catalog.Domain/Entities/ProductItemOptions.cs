using Common.Domain.Entities;
using Modules.Catalog.Domain.DomainEvents;

namespace Modules.Catalog.Domain.Entities;

public class ProductItemOptions : Entity
{
    public Guid ProductItemId { get; private set; }
    public virtual ProductItem ProductItem { get; set; } = default!;
    public Guid SpecificationId { get; private set; }
    public string Value { get; private set; } = string.Empty;
    public virtual SpecificationOption SpecificationOptions { get; set; } = default!;
    public static ProductItemOptions Create(Guid productItemId, Guid specificationId, string value)
    {
        var x = new ProductItemOptions
        {
            ProductItemId = productItemId,
            SpecificationId = specificationId,
            Value = value
        };
        x.RaiseDomainEvent(new ProductItemOptionCreatedDomainEvent(productItemId, specificationId, value));
        return x;
    }
}
