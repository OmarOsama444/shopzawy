using Common.Application;
using Common.Application.Messaging;
using Common.Domain.Exceptions;
using Dapper;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Application.UseCases.Products.Projections;

public class ProductItemOptionCreatedDomainEventHandler(
    IProductItemOptionsRepository productItemOptionsRepository,
    ISpecOptionRepository specOptionRepository,
    IDbConnectionFactory dbConnectionFactory)
    : IDomainEventHandler<ProductItemOptionCreatedDomainEvent>
{
    public async Task Handle(ProductItemOptionCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {

        var productItemOption = await productItemOptionsRepository.GetByIdAndValueAndSpecId(domainEvent.ProductItemOptionId, domainEvent.Value, domainEvent.SpecificationId) ?? throw new SkillHiveException($"Product item option with ID {domainEvent.ProductItemOptionId} not found.");
        var specificationOption = await specOptionRepository.GetBySpecIdAndValue(domainEvent.SpecificationId, domainEvent.Value) ?? throw new SkillHiveException($"Specification option with ID {productItemOption.Id} and value {productItemOption.Value} not found.");
        using var connection = await dbConnectionFactory.CreateSqlConnection();
        string sql = $"UPDATE {Schemas.Orders}.specification_statistics SET total_products = total_products + 1 WHERE id = @Id AND value = @Value";
        var parameters = new { Id = specificationOption.SpecificationId, Value = specificationOption.Value };
        await connection.ExecuteAsync(sql, parameters);
    }
}

