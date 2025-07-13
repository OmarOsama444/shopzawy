using Common.Domain.DomainEvent;

namespace Modules.Catalog.Domain.DomainEvents;

public class ColorCreatedDomainEvent(string name, string code) : DomainEvent
{
    public string Name { get; set; } = name;
    public string Code { get; set; } = code;
}
