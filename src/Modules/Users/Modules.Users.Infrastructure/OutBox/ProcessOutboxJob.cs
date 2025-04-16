using System.Data;
using System.Data.Common;
using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Modules.Common.Application.Clock;
using Modules.Common.Domain.DomainEvent;
using Modules.Common.Infrastructure.Outbox;
using Modules.Users.Application.Abstractions;
using Newtonsoft.Json;
using Quartz;

namespace Modules.Users.Infrastructure.OutBox;

// doesn't allow concurent execution of the same job
[DisallowConcurrentExecution]
public class ProcessOutboxJob(
    IOptions<OutBoxOptions> options,
    IDbConnectionFactory dbConnectionFactory,
    IServiceScopeFactory serviceScopeFactory,
    IDateTimeProvider dateTimeProvider,
    ILogger<ProcessOutboxJob> logger) : IJob
{
    private const string ModuleName = UsersModule.SchemaName;
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
        SELECT TOP {options.Value.BatchSize} * 
        FROM {ModuleName}.outbox_messages 
        WITH (UPDLOCK, ROWLOCK)
        WHERE {nameof(OutboxMessage.ProcessedOnUtc)} IS NULL
        ORDER BY {nameof(OutboxMessage.OccurredOnUtc)}
        """;
        var OutboxMessages = await connection.QueryAsync<OutboxMessage>(query, new { }, dbTransaction);
        return OutboxMessages.ToList();
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
            {nameof(OutboxMessage.ProcessedOnUtc)} = @ProcessedOnUtc ,
            {nameof(OutboxMessage.Error)} = @Error
        WHERE {nameof(OutboxMessage.Id)} = @Id
        """;

        await connection.ExecuteAsync(query, new
        {
            ProcessedOnUtc = dateTimeProvider.UtcNow,
            Error = exception?.Message,
            Id = outboxMessage.Id
        }, dbTransaction);
    }

}

