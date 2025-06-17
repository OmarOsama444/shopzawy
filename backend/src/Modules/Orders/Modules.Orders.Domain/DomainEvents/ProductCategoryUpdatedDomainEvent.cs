using Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class ProductCategoryUpdatedDomainEvent : DomainEvent
{
    public string CategoryName { get; set; }
    public ProductCategoryUpdatedDomainEvent(string categoryName)
    {
        this.CategoryName = categoryName;
    }
}
