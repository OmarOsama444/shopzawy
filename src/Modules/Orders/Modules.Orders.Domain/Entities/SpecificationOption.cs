using Modules.Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class SpecificationOption : Entity
{
    public Guid Id { get; private set; }
    public Guid SpecificationId { get; private set; }
    public string Value { get; private set; } = string.Empty;
    public virtual Specification Specification { get; set; } = default!;
    public virtual ICollection<ProductItemOptions> ProductItemOptions { get; set; } = [];
    public static SpecificationOption Create(string value, Guid SpecId)
    {
        return new SpecificationOption()
        {
            Value = value,
            SpecificationId = SpecId
        };
    }
}
