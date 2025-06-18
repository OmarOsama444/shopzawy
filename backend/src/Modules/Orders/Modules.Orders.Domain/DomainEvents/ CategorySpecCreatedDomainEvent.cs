using Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class CategorySpecCreatedDomainEvent : DomainEvent
{
    public Guid CategorySpecId { get; init; }

    public CategorySpecCreatedDomainEvent(Guid categorySpecId)
    {
        CategorySpecId = categorySpecId;
    }
}
