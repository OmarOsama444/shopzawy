using Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class ProductItemUpdatedDomainEvent : DomainEvent
{
    public Guid ProductItemId { get; set; }
    public ProductItemUpdatedDomainEvent(Guid ProductItemId)
    {
        this.ProductItemId = ProductItemId;
    }
}
