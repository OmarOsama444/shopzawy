using Modules.Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class SpecificationTranslation : Entity
{
    public Guid Id { get; set; }
    public Guid SpecId { get; set; }
    public string LangCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public virtual Specification specification { get; set; } = default!;
    public static SpecificationTranslation Create(Guid specId, string langCode, string name)
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
