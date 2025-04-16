using Modules.Common.Domain.DomainEvent;

namespace Modules.Orders.Domain.DomainEvents;

// TODO make it add the newly created color to the category spec options where datatype = color 
public class ColorCreatedDomainEvent : DomainEvent
{
    public string name { get; set; }
    public ColorCreatedDomainEvent(string name)
    {
        this.name = name;
    }
}
