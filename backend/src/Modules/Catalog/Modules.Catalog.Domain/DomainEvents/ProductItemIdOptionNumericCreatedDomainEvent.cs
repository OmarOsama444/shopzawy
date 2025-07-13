using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents;

public class ProductItemIdOptionNumericCreatedDomainEvent(Guid productItemId, Guid SpecificationID, float NumericValue) : DomainEvent
{
    public Guid ProductItemId { get; set; } = productItemId;
    public Guid SpecificationId { get; set; } = SpecificationID;
    public float NumericValue { get; set; } = NumericValue;
}