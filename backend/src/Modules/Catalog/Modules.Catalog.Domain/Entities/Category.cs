using Common.Domain.Entities;
using Modules.Catalog.Domain.DomainEvents;
using Modules.Catalog.Domain.Entities.Translation;

namespace Modules.Catalog.Domain.Entities;

public class Category : Entity
{
    public int Id { get; set; } 
    public int? ParentCategoryId { get; set; } = null;
    public int Order { get; set; } = int.MaxValue;
    public DateTime CreatedOn { get; set; }
    public List<int> Path { get; set; } = [];
    public virtual Category ParentCategory { get; set; } = default!;
    public virtual ICollection<Category> ChilrenCategories { get; set; } = [];
    public virtual ICollection<Product> Products { get; set; } = [];
    public virtual ICollection<CategorySpec> CategorySpecs { get; set; } = [];
    public virtual ICollection<CategoryTranslation> CategoryTranslations { get; set; } = [];
    public static Category Create(
       int order,
       Category? parentCategory = null)
    {
        var category = new Category()
        {
            Id = 0,
            Order = order,
            CreatedOn = DateTime.UtcNow,
            ParentCategoryId = parentCategory?.Id,
            Path = parentCategory == null ? [] : [.. parentCategory.Path, parentCategory.Id]
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
            Id = 1,
            Order = int.MaxValue,
            CreatedOn = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            ParentCategoryId = null,
            Path = []
        };
    }
}
