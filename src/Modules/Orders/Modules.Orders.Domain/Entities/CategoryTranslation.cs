namespace Modules.Orders.Domain.Entities;

public class CategoryTranslation
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string LangCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public virtual Category Category { get; set; } = default!;
    public static CategoryTranslation Create(Guid categoryId, string langCode, string name, string Description, string ImageUrl)
    {
        return new CategoryTranslation
        {
            CategoryId = categoryId,
            LangCode = langCode,
            Name = name,
            Description = Description,
            ImageUrl = ImageUrl
        };
    }
}
