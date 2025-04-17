using Modules.Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

public class ColorCreatedDomainEvent : DomainEvent
{
    public string name { get; set; }
    public ColorCreatedDomainEvent(string name)
    {
        this.name = name;
    }
}
