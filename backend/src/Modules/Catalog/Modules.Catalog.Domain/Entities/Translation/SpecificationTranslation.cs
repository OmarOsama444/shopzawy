using Common.Domain.Entities;
using Common.Domain.ValueObjects;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Domain.Entities.Translation;

public class SpecificationTranslation : Entity
{
    public Guid Id { get; set; }
    public Guid SpecId { get; set; }
    public Language LangCode { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual Specification specification { get; set; } = default!;
    public static SpecificationTranslation Create(Guid specId, Language langCode, string name)
    {
        return new SpecificationTranslation
        {
            Id = Guid.NewGuid(),
            SpecId = specId,
            Name = name,
            LangCode = langCode
        };
    }
}
