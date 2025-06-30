using Common.Domain.Entities;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Domain.Entities;

public class Color : Entity
{
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedOn { get; private set; }
    public virtual ICollection<ProductItemOptionColor> ProductItemOptionColors { get; set; } = [];
    public static Color Create(string code, string name)
    {
        var color = new Color { Code = code, Name = name, CreatedOn = DateTime.UtcNow };
        color.RaiseDomainEvent(new ColorCreatedDomainEvent(color.Name, color.Code));
        return color;
    }
}
