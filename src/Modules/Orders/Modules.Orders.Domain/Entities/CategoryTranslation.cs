using Modules.Common.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Domain.Entities;

public class CategoryTranslation : Entity
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Language LangCode { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public virtual Category Category { get; set; } = default!;
    public static CategoryTranslation Create(Guid categoryId, Language langCode, string name, string Description, string ImageUrl)
    {
        return new CategoryTranslation
        {
            Id = Guid.NewGuid(),
            CategoryId = categoryId,
            LangCode = langCode,
            Name = name,
            Description = Description,
            ImageUrl = ImageUrl
        };
    }
    public void Update(string? name, string? description, string? imageUrl)
    {
        if (!string.IsNullOrEmpty(name))
            Name = name;
        if (!string.IsNullOrEmpty(description))
            Description = description;
        if (!string.IsNullOrEmpty(imageUrl))
            ImageUrl = imageUrl;
    }
}
