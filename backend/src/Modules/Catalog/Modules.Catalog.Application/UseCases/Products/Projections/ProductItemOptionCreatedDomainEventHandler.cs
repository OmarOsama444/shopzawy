using Common.Application;
using Common.Application.Messaging;
using Dapper;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Domain.DomainEvents;
using Modules.Catalog.Domain.ValueObjects;

namespace Modules.Catalog.Application.UseCases.Products.Projections;

public class ProductItemOptionCreatedDomainEventHandler(
    IDbConnectionFactory dbConnectionFactory)
    : IDomainEventHandler<ProductItemOptionCreatedDomainEvent>
{
    public async Task Handle(ProductItemOptionCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {

        await using var connection = await dbConnectionFactory.CreateSqlConnection();
        string query =
        $"""
        INSERT INTO Orders.specification_statistics (id, value, data_type, total_products, created_on_utc)
        VALUES (@Id, @Value, @DataType, 1, @CreationDate)
        ON CONFLICT (id, value)
        DO UPDATE SET total_products = Orders.specification_statistics.total_products + 1;
        """;
        await connection.ExecuteAsync(query, new { Id = domainEvent.SpecificationId, Value = domainEvent.Value.ToString(), CreationDate = domainEvent.CreatedOnUtc, DataType = SpecDataType.String });
    }
}

