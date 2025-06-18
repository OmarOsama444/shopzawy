using Common.Application;
using Common.Application.Messaging;
using Common.Domain.Exceptions;
using Dapper;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Application.UseCases.Categories.Projections;

public class CategoryCreatedDomainEventHandler(
    ICategoryRepository categoryRepository,
    IDbConnectionFactory dbConnectionFactory) : IDomainEventHandler<CategoryCreatedDomainEvent>
{
    public async Task Handle(CategoryCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(domainEvent.CategoryId);
        if (category is null)
            throw new SkillHiveException($"Product with ID {category?.Id} not found.");

        if (category.ParentCategoryId is null)
            return; // No parent category, nothing to update

        await using var connection = await dbConnectionFactory.CreateSqlConnection();
        string UpdateQuery = $"""
        UPDATE
            {Schemas.Orders}.category_statistics
        SET
            total_children = total_children + 1
        WHERE
            id = @categoryId;
        """;
        _ = await
            connection
            .ExecuteAsync(UpdateQuery, new { categoryId = category.ParentCategoryId });
    }

}
