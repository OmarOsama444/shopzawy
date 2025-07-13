using System.Data.Common;
using Common.Domain.Entities;
using Common.Domain.ValueObjects;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Domain.Entities.Translation;

public class CategoryTranslation : Entity
{
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
            CategoryId = categoryId,
            LangCode = langCode,
            Name = name,
            Description = Description,
            ImageUrl = ImageUrl
        };
    }
    public static CategoryTranslation Seed()
    {
        return new CategoryTranslation
        {
            CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            LangCode = Language.en,
            Name = "",
            Description = "",
            ImageUrl = ""
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
