using System.Net.Http.Headers;
using Modules.Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class CategorySpec : Entity
{
    public Guid Id { get; private set; }
    public string CategoryName { get; private set; } = string.Empty;
    public Guid SpecId { get; private set; }
    public virtual Category Category { get; set; } = default!;
    public virtual Specification Specification { get; set; } = default!;
    public static CategorySpec Create(string categoryName, Guid specId)
    {
        return new CategorySpec()
        {
            Id = Guid.NewGuid(),
            SpecId = specId,
            CategoryName = categoryName
        };
    }
}
