using Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class ProductItemInStockDomainEvent : DomainEvent
{
    public Guid ProductItemId { get; set; }
    public ProductItemInStockDomainEvent(Guid ProductItemId)
    {
        this.ProductItemId = ProductItemId;
    }
}
