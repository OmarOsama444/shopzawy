using Common.Domain.Entities;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Domain.Entities;

public class ProductItemOptionNumeric : Entity
{
    public Guid ProductItemId { get; set; }
    public Guid SpecificationId { get; set; }
    public float Value { get; set; }
    public virtual ProductItem ProductItem { get; set; } = default!;
    public virtual Specification Specification { get; set; } = default!;
    public static ProductItemOptionNumeric Create(Guid productItemId, Guid SpecificationID, float Value)
    {
        var pioc = new ProductItemOptionNumeric
        {
            ProductItemId = productItemId,
            SpecificationId = SpecificationID,
            Value = Value
        };
        pioc.RaiseDomainEvent(new ProductItemIdOptionNumericCreatedDomainEvent(productItemId, SpecificationID, Value));
        return pioc;
    }
}
