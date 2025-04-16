using Modules.Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class CategorySpec : Entity
{
    public Guid Id { get; private set; }
    public string CategoryName { get; private set; } = string.Empty;
    public Guid SpecId { get; private set; }
    public virtual Category Category { get; set; } = default!;
    public virtual Specification Spec { get; set; } = default!;
    public static CategorySpec Create(Guid specId, string CategoryName)
    {
        return new CategorySpec()
        {
            CategoryName = CategoryName,
            SpecId = specId,
            Id = Guid.NewGuid()
        };
    }
}
