using Common.Application;
using Common.Application.Messaging;
using Common.Domain.Exceptions;
using Dapper;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.DomainEvents;
using Modules.Orders.Domain.Entities.Views;

namespace Modules.Orders.Application.Projections;

public class ProductItemOptionCreatedDomainEventHandler(
    IProductItemOptionsRepository productItemOptionsRepository,
    ISpecStatisticRepository specStatisticRepository,
    IDbConnectionFactory dbConnectionFactory)
    : IDomainEventHandler<ProductItemOptionCreatedDomainEvent>
{
    public async Task Handle(ProductItemOptionCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {

        var productItemOption = await productItemOptionsRepository.GetByIdAsync(domainEvent.ProductItemOptionId);
        if (productItemOption is null)
        {
            throw new SkillHiveException($"Product item option with ID {domainEvent.ProductItemOptionId} not found.");
        }
        var specificationOption = await specStatisticRepository.GetByIdAndValueAsync(productItemOption.Id, productItemOption.Value, cancellationToken);
        if (specificationOption is null)
        {
            throw new SkillHiveException($"Specification option with ID {productItemOption.Id} and value {productItemOption.Value} not found.");
        }
        using var connection = await dbConnectionFactory.CreateSqlConnection();
        string sql = $"UPDATE {Schemas.Orders}.specification_statistic SET total_products = total_products + 1 WHERE id = @Id AND value = @Value";
        var parameters = new { Id = specificationOption.Id, Value = specificationOption.Value };
        await connection.ExecuteAsync(sql, parameters);
    }
}

