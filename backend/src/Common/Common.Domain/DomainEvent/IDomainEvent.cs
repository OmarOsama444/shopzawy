using MediatR;

namespace Common.Domain.DomainEvent;

public interface IDomainEvent : INotification
{
    public Guid Id { get; }
    public DateTime CreatedOnUtc { get; }
}
