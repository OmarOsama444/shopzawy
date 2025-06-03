﻿using Modules.Common.Domain.DomainEvent;

namespace Modules.Common.Domain.Entities;

public interface IEntity
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
    public void RaiseDomainEvent(IDomainEvent domainEvent);
}

public abstract class Entity : IEntity
{
    private readonly List<IDomainEvent> domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.ToList();
    public void ClearDomainEvents()
    {
        domainEvents.Clear();
    }
    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }
}
