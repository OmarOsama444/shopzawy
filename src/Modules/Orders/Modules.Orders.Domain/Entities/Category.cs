using Modules.Common.Domain.Entities;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Domain.Entities;

public class Category : Entity
{
    public string? ParentCategoryName { get; private set; }
    public string CategoryName { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int Order { get; private set; }
    public string? ImageUrl { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public string CategoryPath { get; private set; } = string.Empty;
    // TODO ENTITY CONFIG
    public virtual Category ParentCategory { get; set; } = default!;
    public virtual ICollection<Category> ChilrenCategories { get; set; } = [];
    public virtual ICollection<Product> Products { get; set; } = [];
    public virtual ICollection<CategorySpec> CategorySpecs { get; set; } = [];
    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = [];
    public static Category Create(
       string CategoryName,
       string Description,
       int Order,
       string? ImageUrl = null,
       Category? parentCategory = null)
    {
        if (parentCategory is null)
        {
            var category = new Category()
            {
                CategoryName = CategoryName,
                Description = Description,
                Order = Order,
                ImageUrl = ImageUrl,
                CreatedOn = DateTime.UtcNow
            };
            category.RaiseDomainEvent(new CategoryCreatedDomainEvent(CategoryName));
            return category;
        }
        else
        {
            var category = new Category()
            {
                CategoryName = CategoryName,
                Description = Description,
                Order = Order,
                ImageUrl = ImageUrl,
                CreatedOn = DateTime.UtcNow,
                ParentCategoryName = parentCategory.CategoryName,
                CategoryPath =
                    (
                        string.IsNullOrEmpty(parentCategory.CategoryPath)
                        ? ""
                        : (parentCategory.CategoryPath + ",")
                    ) +
                    parentCategory.CategoryName,
            };
            category.RaiseDomainEvent(new CategoryCreatedDomainEvent(CategoryName));
            return category;
        }
    }

    public void Update(string? Description, int? Order, string? ImageUrl)
    {
        if (!String.IsNullOrEmpty(Description))
            this.Description = Description;

        if (Order.HasValue)
            this.Order = Order.Value;

        if (!String.IsNullOrEmpty(ImageUrl))
            this.ImageUrl = ImageUrl;
    }

}
