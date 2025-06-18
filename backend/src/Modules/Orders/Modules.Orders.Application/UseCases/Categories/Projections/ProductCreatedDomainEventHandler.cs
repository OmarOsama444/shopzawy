using Common.Application;
using Common.Application.Messaging;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Dapper;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.DomainEvents;
using Modules.Orders.Domain.Entities.Views;

namespace Modules.Orders.Application.UseCases.Categories.Projections;

public class ProductCreatedDomainEventHandler(
    IDbConnectionFactory dbConnectionFactory
    , IProductRepository productRepository) : IDomainEventHandler<ProductCreatedDomainEvent>
{
    public async Task Handle(ProductCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var productId = domainEvent.ProductId;
        var product = await productRepository.GetByIdAsync(productId);
        if (product is null)
        {
            throw new SkillHiveException($"Product with ID {productId} not found.");
        }
        await using var connection = await dbConnectionFactory.CreateSqlConnection();
        string UpdateQuery = $"""
        UPDATE
            {Schemas.Orders}.category_statistics
        SET
            total_products = total_products + 1
        WHERE
            id = @categoryId;
        """;
        _ = await
            connection
            .ExecuteAsync(UpdateQuery, new { categoryId = product.CategoryId });
    }

}
