using System.Data;
using System.Data.Common;
using Common.Application;
using Common.Application.Clock;
using Common.Domain.DomainEvent;
using Common.Infrastructure.Outbox;
using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Modules.Catalog.Application.Abstractions;
using Newtonsoft.Json;
using Quartz;

namespace Modules.Catalog.Infrastructure.OutBox;

// doesn't allow concurent execution of the same job
[DisallowConcurrentExecution]
public class ProcessOutboxJob(
    IOptions<OutBoxOptions> options,
    IDbConnectionFactory dbConnectionFactory,
    IServiceScopeFactory serviceScopeFactory,
    IDateTimeProvider dateTimeProvider,
    ILogger<ProcessOutboxJob> logger) : IJob
{
    private const string ModuleName = Schemas.Catalog;
    public async Task Execute(IJobExecutionContext context)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.CreateSqlConnection();
        await using DbTransaction dbTransaction = await dbConnection.BeginTransactionAsync();
        var outbox_messages = await GetOutboxMessagesAsync(dbConnection, dbTransaction);
        logger.LogInformation("{Module} - Beginning to process outbox messages", ModuleName);

        foreach (OutboxMessage outboxMessage in outbox_messages)
        {
            Exception? exception = null;
            try
            {
                IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    }
                )!;
                using IServiceScope serviceScope = serviceScopeFactory.CreateScope();
                IPublisher publisher = serviceScope.ServiceProvider.GetRequiredService<IPublisher>();
                await publisher.Publish(domainEvent);
            }
            catch (Exception caughtException)
            {
                exception = caughtException;
                logger.LogError(exception, "{Module} - Exception while processing outbox message {MessageId}", ModuleName, outboxMessage.Id);
            }
            finally
            {
                await UpdateOutboxMessage(dbConnection, dbTransaction, outboxMessage, exception);
            }
        }
        await dbTransaction.CommitAsync();
        logger.LogInformation("{Module} - Completed processing the outbox message ", ModuleName);
    }
    private async Task<IReadOnlyCollection<OutboxMessage>> GetOutboxMessagesAsync(
        IDbConnection connection,
        IDbTransaction dbTransaction)
    {
        string query =
        $"""
        SELECT 
        id AS {nameof(OutboxMessage.Id)},
        type AS {nameof(OutboxMessage.Type)},
        content AS {nameof(OutboxMessage.Content)},
        occurred_on_utc AS {nameof(OutboxMessage.OccurredOnUtc)},
        processed_on_utc AS {nameof(OutboxMessage.ProcessedOnUtc)},
        error AS Error
        FROM {ModuleName}.outbox_messages
        WHERE "processed_on_utc" IS NULL
        ORDER BY "occurred_on_utc"
        LIMIT {options.Value.BatchSize}
        FOR UPDATE ;
        """;
        var OutboxMessages = await connection.QueryAsync<OutboxMessage>(query, new { }, dbTransaction);
        return [.. OutboxMessages];
    }

    private async Task UpdateOutboxMessage(
        IDbConnection connection,
        IDbTransaction dbTransaction,
        OutboxMessage outboxMessage,
        Exception? exception
    )
    {
        const string query =
        $"""
        UPDATE 
            {ModuleName}.outbox_messages
        SET
            processed_on_utc = @ProcessedOnUtc ,
            error = @Error
        WHERE id = @Id
        """;

        await connection.ExecuteAsync(query, new
        {
            ProcessedOnUtc = dateTimeProvider.UtcNow,
            Error = exception?.Message,
            Id = outboxMessage.Id
        }, dbTransaction);
    }

}

