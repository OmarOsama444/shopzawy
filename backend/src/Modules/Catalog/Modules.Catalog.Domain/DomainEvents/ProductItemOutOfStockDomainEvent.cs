using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents;

public class ProductItemOutOfStockDomainEvent(Guid ProductItemId) : DomainEvent
{
    public Guid ProductItemId { get; set; } = ProductItemId;
}
