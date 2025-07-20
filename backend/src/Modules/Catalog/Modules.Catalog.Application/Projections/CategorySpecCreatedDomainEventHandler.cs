using Common.Application;
using Common.Application.Messaging;
using Dapper;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Domain.DomainEvents;

namespace Modules.Catalog.Application.Projections;

public class CategorySpecCreatedDomainEventHandler(
    IDbConnectionFactory dbConnectionFactory
) : IDomainEventHandler<CategorySpecCreatedDomainEvent>
{
    public async Task Handle(CategorySpecCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await using var connection = await dbConnectionFactory.CreateSqlConnection();
        string UpdateQuery = $"""
        UPDATE
            {Schemas.Catalog}.category_statistics
        SET
            total_specs = total_specs + 1
        WHERE
            id = @categoryId;
        """;
        _ = await
            connection
            .ExecuteAsync(UpdateQuery, new { categoryId = notification.CategoryId });
    }

}
