using Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class CategoryCreatedDomainEvent : DomainEvent
{
    public Guid CategoryId { get; private set; }
    public CategoryCreatedDomainEvent(Guid CategoryId)
    {
        this.CategoryId = CategoryId;
    }
}
