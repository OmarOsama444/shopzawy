using Modules.Common.Domain.DomainEvent;

namespace Modules.Common.Domain.Entities;

public abstract class Entity
{
    private readonly List<IDomainEvent> domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.ToList();
    public void ClearDomainEvents()
    {
        domainEvents.Clear();
    }
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }
}
