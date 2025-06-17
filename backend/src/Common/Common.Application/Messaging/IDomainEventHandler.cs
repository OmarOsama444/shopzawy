using Common.Domain.DomainEvent;
using MediatR;

namespace Common.Application.Messaging;

public interface IDomainEventHandler<in T> : INotificationHandler<T> where T : IDomainEvent;