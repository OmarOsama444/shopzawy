using System.Net.Http.Headers;
using Common.Domain.Entities;
using Modules.Catalog.Domain.DomainEvents;

namespace Modules.Catalog.Domain.Entities;

public class CategorySpec : Entity
{
    public int CategoryId { get; private set; }
    public Guid SpecId { get; private set; }
    public virtual Category Category { get; set; } = default!;
    public virtual Specification Specification { get; set; } = default!;
    public static CategorySpec Create(int categoryId, Guid specId)
    {
        var categorySpec = new CategorySpec()
        {
            SpecId = specId,
            CategoryId = categoryId
        };
        categorySpec.RaiseDomainEvent(new CategorySpecCreatedDomainEvent( categoryId , specId ));
        return categorySpec;
    }
}
