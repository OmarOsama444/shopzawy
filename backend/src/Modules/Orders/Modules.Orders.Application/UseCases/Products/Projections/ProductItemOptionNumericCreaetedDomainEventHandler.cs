using Common.Application;
using Common.Application.Messaging;
using Dapper;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.DomainEvents;
using Modules.Orders.Domain.ValueObjects;
using Nest;

namespace Modules.Orders.Application.UseCases.Products.Projections;

public class ProductItemOptionNumericCreaetedDomainEventHandler(IDbConnectionFactory dbConnectionFactory) : IDomainEventHandler<ProductItemIdOptionNumericCreatedDomainEvent>
{
    public async Task Handle(ProductItemIdOptionNumericCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await using var connection = await dbConnectionFactory.CreateSqlConnection();
        string query =
        $"""
        INSERT INTO {Schemas.Orders}.specification_statistics (id, value, data_type , total_products , created_on_utc )
        VALUES (@Id, @Value , @DataType , 1 , @CreationDate )
        ON CONFLICT (id, value)
        DO UPDATE SET total_products = total_products + 1;
        """;
        await connection.ExecuteAsync(query, new { Id = notification.SpecificationId, Value = notification.NumericValue.ToString(), CreationDate = notification.CreatedOnUtc, DataType = SpecDataType.Number });
    }
}
