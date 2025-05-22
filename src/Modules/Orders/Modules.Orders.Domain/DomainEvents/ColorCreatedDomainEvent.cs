using Modules.Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class ColorCreatedDomainEvent : DomainEvent
{
    public string name { get; set; }
    public string code { get; set; }
    public ColorCreatedDomainEvent(string name, string code)
    {
        this.name = name;
        this.code = code;
    }
}
