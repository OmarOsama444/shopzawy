using Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class ProductCreatedDomainEvent : DomainEvent
{
    public Guid ProductId { get; init; }

    public ProductCreatedDomainEvent(Guid productId)
    {
        ProductId = productId;
    }
}
