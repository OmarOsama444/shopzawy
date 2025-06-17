using Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class ProductItemCreatedDomainEvent : DomainEvent
{
    public Guid ProductItemId { get; set; }
    public ProductItemCreatedDomainEvent(Guid ProductItemId)
    {
        this.ProductItemId = ProductItemId;
    }
}
