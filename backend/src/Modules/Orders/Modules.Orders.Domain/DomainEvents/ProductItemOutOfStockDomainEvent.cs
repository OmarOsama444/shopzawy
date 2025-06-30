using Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class ProductItemOutOfStockDomainEvent : DomainEvent
{
    public Guid ProductItemId { get; set; }
    public ProductItemOutOfStockDomainEvent(Guid ProductItemId)
    {
        this.ProductItemId = ProductItemId;
    }
}
