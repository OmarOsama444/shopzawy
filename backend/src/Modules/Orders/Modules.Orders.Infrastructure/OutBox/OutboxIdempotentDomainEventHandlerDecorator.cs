using Common.Application;
using Common.Domain.DomainEvent;
using Common.Infrastructure.Outbox;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.Orders.Application.Abstractions;

namespace Modules.Orders.Infrastructure.OutBox;

public class OutboxIdempotentDomainEventHandlerDecorator<TDomainEvent>(
    INotificationHandler<TDomainEvent> innerHandler,
    IDbConnectionFactory dbConnectionFactory,
    ILogger<OutboxIdempotentDomainEventHandlerDecorator<TDomainEvent>> logger) : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public const string ModuleName = Schemas.Orders;
    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        await using var connection = await dbConnectionFactory.CreateSqlConnection();

        IDomainEvent domainEvent = notification;
        var outboxConsumerMessage = new OutboxConsumerMessage
        {
            id = domainEvent.Id,
            HandlerName = innerHandler.GetType().Name
        };

        const string query = $"SELECT COUNT(1) FROM {ModuleName}.Outbox_ConsumerMessages WHERE id = @Id AND HandlerName = @HandlerName";
        var exists = await connection.ExecuteScalarAsync<int>(query, new
        {
            Id = outboxConsumerMessage.id,
            HandlerName = outboxConsumerMessage.HandlerName
        });

        if (exists > 0)
        {
            logger.LogWarning("Duplicate event detected: {EventType} with ID {EventId}. Skipping processing.", typeof(TDomainEvent).Name, outboxConsumerMessage.id);
            return;
        }

        logger.LogInformation("Processing event {EventType} with ID {EventId}.", typeof(TDomainEvent).Name, outboxConsumerMessage.id);
        await innerHandler.Handle(notification, cancellationToken);

        const string insertQuery = $"INSERT INTO {ModuleName}.OutboxConsumerMessages (id, HandlerName) VALUES (@Id, @HandlerName)";
        await connection.ExecuteAsync(insertQuery, outboxConsumerMessage);

        logger.LogInformation("Stored event {EventType} with ID {EventId} in OutboxConsumerMessages.", typeof(TDomainEvent).Name, outboxConsumerMessage.id);

    }

}