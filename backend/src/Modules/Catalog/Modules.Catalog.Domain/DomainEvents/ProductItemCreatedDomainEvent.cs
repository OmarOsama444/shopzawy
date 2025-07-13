using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents;

public class ProductItemCreatedDomainEvent(Guid ProductItemId) : DomainEvent
{
    public Guid ProductItemId { get; set; } = ProductItemId;
}
