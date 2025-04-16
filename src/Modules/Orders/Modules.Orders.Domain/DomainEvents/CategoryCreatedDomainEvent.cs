using Modules.Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class CategoryCreatedDomainEvent : DomainEvent
{
    public string CategoryName { get; private set; }
    public CategoryCreatedDomainEvent(string CategoryId)
    {
        this.CategoryName = CategoryId;
    }
}
