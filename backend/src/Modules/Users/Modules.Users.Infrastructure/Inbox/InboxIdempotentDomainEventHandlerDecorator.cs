using Common.Application;
using Common.Domain.DomainEvent;
using Common.Infrastructure.Inbox;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Users.Application.Abstractions;

namespace Modules.Users.Infrastructure.Inbox;

public class InboxIdempotentDomainEventHandlerDecorator<TDomainEvent>(
    INotificationHandler<TDomainEvent> innerHandler,
    IDbConnectionFactory dbConnectionFactory,
    ILogger<InboxIdempotentDomainEventHandlerDecorator<TDomainEvent>> logger) : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public const string ModuleName = Schemas.Users;
    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        await using var connection = await dbConnectionFactory.CreateSqlConnection();

        IDomainEvent domainEvent = notification;
        var InboxConsumerMessage = new InboxConsumerMessage
        {
            id = domainEvent.Id,
            HandlerName = innerHandler.GetType().Name
        };

        const string query = $"SELECT COUNT(1) FROM {ModuleName}.InboxConsumerMessages WHERE id = @Id AND HandlerName = @HandlerName";
        var exists = await connection.ExecuteScalarAsync<int>(query, new
        {
            Id = InboxConsumerMessage.id,
            HandlerName = InboxConsumerMessage.HandlerName
        });

        if (exists > 0)
        {
            logger.LogWarning("Duplicate event detected: {EventType} with ID {EventId}. Skipping processing.", typeof(TDomainEvent).Name, InboxConsumerMessage.id);
            return;
        }

        logger.LogInformation("Processing event {EventType} with ID {EventId}.", typeof(TDomainEvent).Name, InboxConsumerMessage.id);
        await innerHandler.Handle(notification, cancellationToken);

        const string insertQuery = $"INSERT INTO {ModuleName}.InboxConsumerMessages (id, HandlerName) VALUES (@Id, @HandlerName)";
        await connection.ExecuteAsync(insertQuery, InboxConsumerMessage);

        logger.LogInformation("Stored event {EventType} with ID {EventId} in InboxConsumerMessages.", typeof(TDomainEvent).Name, InboxConsumerMessage.id);

    }

}