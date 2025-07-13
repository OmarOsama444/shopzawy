using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents;

public class CategoryCreatedDomainEvent(Guid CategoryId) : DomainEvent
{
    public Guid CategoryId { get; private set; } = CategoryId;
}
