using Common.Application;
using Common.Domain.DomainEvent;
using Common.Infrastructure.Outbox;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.Catalog.Application.Abstractions;

namespace Modules.Catalog.Infrastructure.OutBox;

public class OutboxIdempotentDomainEventHandlerDecorator<TDomainEvent>(
    INotificationHandler<TDomainEvent> innerHandler,
    IDbConnectionFactory dbConnectionFactory,
    ILogger<OutboxIdempotentDomainEventHandlerDecorator<TDomainEvent>> logger) : IIdempotentDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public const string ModuleName = Schemas.Catalog;
    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        await using var connection = await dbConnectionFactory.CreateSqlConnection();

        IDomainEvent domainEvent = notification;
        var outboxConsumerMessage = new OutboxConsumerMessage
        {
            id = domainEvent.Id,
            HandlerName = innerHandler.GetType().Name
        };

        const string query = $"SELECT COUNT(1) FROM {ModuleName}.outbox_consumer_message WHERE id = @id AND handler_name = @HandlerName";
        var exists = await connection.ExecuteScalarAsync<int>(query, outboxConsumerMessage);

        if (exists > 0)
        {
            logger.LogWarning("Duplicate event detected: {EventType} with ID {EventId}. Skipping processing.", typeof(TDomainEvent).Name, outboxConsumerMessage.id);
            return;
        }

        logger.LogInformation("Processing event {EventType} with ID {EventId}.", typeof(TDomainEvent).Name, outboxConsumerMessage.id);
        await innerHandler.Handle(notification, cancellationToken);

        const string insertQuery = $"INSERT INTO {ModuleName}.outbox_consumer_message (id, handler_name) VALUES (@Id, @HandlerName)";
        await connection.ExecuteAsync(insertQuery, outboxConsumerMessage);

        logger.LogInformation("Stored event {EventType} with ID {EventId} in OutboxConsumerMessages.", typeof(TDomainEvent).Name, outboxConsumerMessage.id);

    }

}