using Modules.Common.Domain.Entities;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Domain.Entities;

public class Category : Entity
{
    public Guid Id { get; private set; }
    public Guid? ParentCategoryId { get; private set; }
    public int Order { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public virtual Category ParentCategory { get; set; } = default!;
    public virtual ICollection<Category> ChilrenCategories { get; set; } = [];
    public virtual ICollection<Product> Products { get; set; } = [];
    public virtual ICollection<CategorySpec> CategorySpecs { get; set; } = [];
    public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; } = [];
    public static Category Create(
       int Order,
       Category? parentCategory = null)
    {

        var category = new Category()
        {
            Id = Guid.NewGuid(),
            Order = Order,
            CreatedOn = DateTime.UtcNow,
            ParentCategoryId = parentCategory?.Id
        };
        category.RaiseDomainEvent(new CategoryCreatedDomainEvent(category.Id));
        return category;

    }

    public void Update(int? Order)
    {
        if (Order.HasValue)
            this.Order = Order.Value;
    }

}
