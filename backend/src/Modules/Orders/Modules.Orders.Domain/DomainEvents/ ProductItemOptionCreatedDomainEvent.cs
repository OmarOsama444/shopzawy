using Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class ProductItemOptionCreatedDomainEvent : DomainEvent
{
    public ProductItemOptionCreatedDomainEvent(Guid productItemOptionId)
    {
        ProductItemOptionId = productItemOptionId;
    }

    public Guid ProductItemOptionId { get; }
}

