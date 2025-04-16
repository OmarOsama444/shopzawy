using System.Data;
using System.Data.Common;
using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Modules.Common.Application.Clock;
using Modules.Common.Domain.DomainEvent;
using Modules.Common.Infrastructure.Inbox;
using Modules.Users.Application.Abstractions;
using Newtonsoft.Json;
using Quartz;

namespace Modules.Users.Infrastructure.Inbox;

[DisallowConcurrentExecution]
public class ProcessInboxJob(
    IDbConnectionFactory dbConnectionFactory,
    ILogger<ProcessInboxJob> logger,
    IOptions<InboxOptions> options,
    IServiceScopeFactory serviceScopeFactory,
    IDateTimeProvider dateTimeProvider) : IJob
{
    private const string ModuleName = UsersModule.SchemaName;
    public async Task Execute(IJobExecutionContext context)
    {

        await using DbConnection dbConnection = await dbConnectionFactory.CreateSqlConnection();
        await using DbTransaction dbTransaction = await dbConnection.BeginTransactionAsync();
        var inbox_messages = await GetInboxMessagesAsync(dbConnection, dbTransaction);
        logger.LogInformation("{Module} - Beginning to process Inbox messages", ModuleName);

        foreach (InboxMessage inboxMessage in inbox_messages)
        {
            Exception? exception = null;
            try
            {
                IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    inboxMessage.Content, new JsonSerializerSettings
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
                logger.LogError(exception, "{Module} - Exception while processing inbox message {MessageId}", ModuleName, inboxMessage.Id);
            }
            finally
            {
                await UpdateInboxMessage(dbConnection, dbTransaction, inboxMessage, exception);
            }
        }
        await dbTransaction.CommitAsync();
        logger.LogInformation("{Module} - Completed processing the inbox message ", ModuleName);
    }
    private async Task<IReadOnlyCollection<InboxMessage>> GetInboxMessagesAsync(
        DbConnection connection,
        DbTransaction dbTransaction)
    {
        string query =
        $"""
        SELECT TOP {options.Value.BatchSize} * 
        FROM {ModuleName}.inbox_messages 
        WITH (UPDLOCK, ROWLOCK)
        WHERE {nameof(InboxMessage.ProcessedOnUtc)} IS NULL
        ORDER BY {nameof(InboxMessage.OccurredOnUtc)}
        """;
        var inboxMessages = await connection.QueryAsync<InboxMessage>(query, new { }, dbTransaction);
        return inboxMessages.ToList();
    }

    private async Task UpdateInboxMessage(
        IDbConnection connection,
        IDbTransaction dbTransaction,
        InboxMessage inboxMessage,
        Exception? exception
    )
    {
        const string query =
        $"""
        UPDATE 
            {ModuleName}.inbox_messages
        SET
            {nameof(InboxMessage.ProcessedOnUtc)} = @ProcessedOnUtc ,
            {nameof(InboxMessage.Error)} = @Error
        WHERE {nameof(InboxMessage.Id)} = @Id
        """;

        await connection.ExecuteAsync(query, new
        {
            ProcessedOnUtc = dateTimeProvider.UtcNow,
            Error = exception?.Message,
            Id = inboxMessage.Id
        }, dbTransaction);
    }
}
