using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents;

public class ProductItemOptionCreatedDomainEvent(Guid productItemOptionId, Guid specificationId, string value) : DomainEvent
{
    public Guid SpecificationId { get; set; } = specificationId;
    public Guid ProductItemOptionId { get; set; } = productItemOptionId;
    public string Value { get; set; } = value;
}

