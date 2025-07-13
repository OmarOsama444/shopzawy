using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents;

public class ProductCategoryUpdatedDomainEvent(string categoryName) : DomainEvent
{
    public string CategoryName { get; set; } = categoryName;
}
