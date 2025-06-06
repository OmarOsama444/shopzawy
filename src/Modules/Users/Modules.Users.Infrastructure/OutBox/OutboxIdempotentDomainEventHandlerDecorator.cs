using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.Common.Domain.DomainEvent;
using Modules.Common.Infrastructure;
using Modules.Common.Infrastructure.Outbox;
using Modules.Users.Application.Abstractions;

namespace Modules.Users.Infrastructure.OutBox;

public class OutboxIdempotentDomainEventHandlerDecorator<TDomainEvent>(
    INotificationHandler<TDomainEvent> innerHandler,
    IDbConnectionFactory dbConnectionFactory,
    ILogger<OutboxIdempotentDomainEventHandlerDecorator<TDomainEvent>> logger) : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public const string ModuleName = Schemas.Users;
    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        await using var connection = await dbConnectionFactory.CreateSqlConnection();

        IDomainEvent domainEvent = notification;
        var outboxConsumerMessage = new OutboxConsumerMessage
        {
            id = domainEvent.Id,
            HandlerName = innerHandler.GetType().Name
        };

        const string query = $"SELECT COUNT(1) FROM {ModuleName}.OutboxConsumerMessages WHERE id = @Id AND HandlerName = @HandlerName";
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