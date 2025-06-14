using Modules.Common.Domain.Entities;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Domain.Entities;

public class Category : Entity
{
    public Guid Id { get; private set; } = Guid.Empty;
    public Guid? ParentCategoryId { get; private set; } = null;
    public int Order { get; private set; } = int.MaxValue;
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;
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
    public static Category Seed()
    {
        return new Category()
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Order = int.MaxValue,
            CreatedOn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            ParentCategoryId = null
        };
    }
}
