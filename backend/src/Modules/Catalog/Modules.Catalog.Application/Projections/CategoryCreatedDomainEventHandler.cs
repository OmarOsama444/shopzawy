using Common.Application;
using Common.Application.Messaging;
using Common.Domain.Exceptions;
using Dapper;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.DomainEvents;

namespace Modules.Catalog.Application.Projections;

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
            return;

        await using var connection = await dbConnectionFactory.CreateSqlConnection();
        string UpdateQuery = $"""
        UPDATE
            {Schemas.Catalog}.category_statistics
        SET
            total_children = total_children + 1
        WHERE
            id = @categoryId;
        """;
        await
            connection
            .ExecuteAsync(UpdateQuery, new { categoryId = category.ParentCategoryId });
    }

}
