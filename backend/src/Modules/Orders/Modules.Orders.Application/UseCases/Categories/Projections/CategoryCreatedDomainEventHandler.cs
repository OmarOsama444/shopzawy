using Common.Application;
using Common.Application.Messaging;
using Common.Domain.Exceptions;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Application.UseCases.Categories.Projections;

public class CategoryCreatedDomainEventHandler(
    IServiceScopeFactory serviceScopeFactory,
    IDbConnectionFactory dbConnectionFactory) : IDomainEventHandler<CategoryCreatedDomainEvent>
{
    public async Task Handle(CategoryCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        ICategoryRepository categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();
        var category = await categoryRepository.GetByIdAsync(domainEvent.CategoryId);
        if (category is null)
            throw new SkillHiveException($"Product with ID {category?.Id} not found.");

        if (category.ParentCategoryId is null)
            return;

        await using var connection = await dbConnectionFactory.CreateSqlConnection();
        string UpdateQuery = $"""
        UPDATE
            {Schemas.Orders}.category_statistics
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
