using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents;

public class CategoryCreatedDomainEvent(int CategoryId) : DomainEvent
{
    public int CategoryId { get; private set; } = CategoryId;
}
