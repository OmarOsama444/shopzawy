using Common.Domain.Entities;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Domain.Entities;

public class SpecificationOption : Entity
{

    public Guid SpecificationId { get; private set; }
    public string Value { get; private set; } = string.Empty;
    public virtual Specification Specification { get; set; } = default!;
    public virtual ICollection<ProductItemOptions> ProductItemOptions { get; set; } = [];
    public static SpecificationOption Create(string value, Guid SpecId)
    {
        var option = new SpecificationOption
        {
            SpecificationId = SpecId,
            Value = value
        };
        option.RaiseDomainEvent(new SpecificationOptionCreatedDomainEvent(
            option
                .SpecificationId,
            option
                .Value));
        return option;
    }
}
