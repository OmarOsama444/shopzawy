using System.Net.Http.Headers;
using Common.Domain.Entities;
using Modules.Catalog.Domain.DomainEvents;

namespace Modules.Catalog.Domain.Entities;

public class CategorySpec : Entity
{
    public Guid Id { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid SpecId { get; private set; }
    public virtual Category Category { get; set; } = default!;
    public virtual Specification Specification { get; set; } = default!;
    public static CategorySpec Create(Guid CategoryId, Guid specId)
    {
        var categorySpec = new CategorySpec()
        {
            Id = Guid.NewGuid(),
            SpecId = specId,
            CategoryId = CategoryId
        };
        categorySpec.RaiseDomainEvent(new CategorySpecCreatedDomainEvent(categorySpec.Id));
        return categorySpec;
    }
}
