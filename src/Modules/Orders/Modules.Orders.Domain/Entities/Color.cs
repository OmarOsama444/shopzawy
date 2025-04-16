using System.Runtime.CompilerServices;
using Modules.Common.Domain.Entities;

namespace Modules.Orders.Domain.Entities;

public class Color : Entity
{
    public string Code { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public static Color Create(string code, string name)
    {
        return new Color { Code = code, Name = name };
    }
}
