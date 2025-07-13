using Common.Domain.Entities;
using Modules.Catalog.Domain.DomainEvents;

namespace Modules.Catalog.Domain.Entities;

public class ProductItemOptionColor : Entity
{
    public Guid ProductItemId { get; set; }
    public Guid SpecificationId { get; set; }
    public string ColorCode { get; set; } = string.Empty;
    public virtual ProductItem ProductItem { get; set; } = default!;
    public virtual Specification Specification { get; set; } = default!;
    public virtual Color Color { get; set; } = default!;
    public static ProductItemOptionColor Create(Guid productItemId, Guid SpecificationID, string ColorCode)
    {
        var pioc = new ProductItemOptionColor
        {
            ProductItemId = productItemId,
            SpecificationId = SpecificationID,
            ColorCode = ColorCode
        };
        pioc.RaiseDomainEvent(new ProductItemOptionColorCreatedDomainEvent(productItemId, SpecificationID, ColorCode));
        return pioc;
    }
}


