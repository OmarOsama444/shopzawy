using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents;

public class ProductCreatedDomainEvent(Guid productId) : DomainEvent
{
    public Guid ProductId { get; init; } = productId;
}
