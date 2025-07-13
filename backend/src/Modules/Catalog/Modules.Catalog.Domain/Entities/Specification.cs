using System.Runtime.InteropServices;
using Common.Domain.Entities;
using Modules.Catalog.Domain.DomainEvents;
using Modules.Catalog.Domain.Entities.Translation;
using Modules.Catalog.Domain.ValueObjects;

namespace Modules.Catalog.Domain.Entities;

public class Specification : Entity
{
    public Guid Id { get; private set; }
    public SpecDataType DataType { get; private set; }
    public virtual ICollection<SpecificationOption> SpecificationOptions { get; set; } = [];
    public virtual ICollection<SpecificationTranslation> Translations { get; set; } = [];
    public virtual ICollection<CategorySpec> CategorySpecs { get; set; } = [];
    public virtual ICollection<ProductItemOptionNumeric> ProductItemOptionNumerics { get; set; } = [];
    public virtual ICollection<ProductItemOptionColor> ProductItemOptionColors { get; set; } = [];
    public static Specification Create(SpecDataType dataType)
    {
        var cat = new Specification() { Id = Guid.NewGuid(), DataType = dataType };
        cat.RaiseDomainEvent(new SpecCreatedDomainEvent(cat.Id));
        return cat;
    }
}
