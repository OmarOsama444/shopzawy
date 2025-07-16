using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents;

public class CategorySpecCreatedDomainEvent : DomainEvent
{
    public int CategoryId { get; init; }
    public Guid SpecId { get; init;  }

    public CategorySpecCreatedDomainEvent(int categoryId, Guid specId)
    {
        this.CategoryId = categoryId;
        this.SpecId = specId; 
    }
}
