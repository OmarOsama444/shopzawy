using Common.Domain.Entities;
using Common.Domain.ValueObjects;

namespace Modules.Catalog.Domain.Entities.Translation;

public class BrandTranslation : Entity
{
    public Guid Id { get; set; }
    public Guid BrandId { get; private set; }
    public Language LangCode { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public virtual Brand Brand { get; private set; } = default!;

    public static BrandTranslation Create(Guid brandId, Language langCode, string name, string description)
    {
        var brandTranslation = new BrandTranslation
        {
            Id = Guid.NewGuid(),
            BrandId = brandId,
            LangCode = langCode,
            Name = name,
            Description = description
        };
        return brandTranslation;
    }
    public void Update(string? name, string? description)
    {
        if (!string.IsNullOrEmpty(name))
            this.Name = name;
        if (!string.IsNullOrEmpty(description))
            this.Description = description;
    }
}