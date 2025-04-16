using MediatR;
using Modules.Common.Domain.DomainEvent;

namespace Modules.Common.Application.Messaging;

public interface IDomainEventHandler<in T> : INotificationHandler<T> where T : IDomainEvent;