using Modules.Common.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Domain.Entities;

public class SpecificationOption : Entity
{

    public Guid SpecificationId { get; private set; }
    public string Value { get; private set; } = string.Empty;
    public int? NumberValue { get; set; }
    public virtual Specification Specification { get; set; } = default!;
    public virtual ICollection<ProductItemOptions> ProductItemOptions { get; set; } = [];
    public static SpecificationOption Create(string value, Guid SpecId)
    {
        var option = new SpecificationOption
        {
            SpecificationId = SpecId,
            Value = value
        };
        option.NumberValue = int.TryParse(value, out int xx) ? xx : null;
        return option;
    }
}
