using System.Runtime.CompilerServices;
using Modules.Common.Domain.Entities;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Domain.Entities;

public class Color : Entity
{
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public static Color Create(string code, string name)
    {
        var color = new Color { Code = code, Name = name };
        color.RaiseDomainEvent(new ColorCreatedDomainEvent(color.Name));
        return color;
    }
}
