using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents;

public class CategorySpecCreatedDomainEvent : DomainEvent
{
    public Guid CategorySpecId { get; init; }

    public CategorySpecCreatedDomainEvent(Guid categorySpecId)
    {
        CategorySpecId = categorySpecId;
    }
}
