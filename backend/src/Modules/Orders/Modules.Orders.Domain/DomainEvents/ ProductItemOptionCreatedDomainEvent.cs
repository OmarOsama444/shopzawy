using Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class ProductItemOptionCreatedDomainEvent : DomainEvent
{
    public ProductItemOptionCreatedDomainEvent(Guid productItemOptionId, Guid specificationId, string value)
    {
        ProductItemOptionId = productItemOptionId;
        SpecificationId = specificationId;
        Value = value;
    }
    public Guid SpecificationId { get; set; }
    public Guid ProductItemOptionId { get; set; }
    public string Value { get; set; }
}

