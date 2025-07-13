using System.Data;
using Common.Application;
using Common.Application.Messaging;
using Common.Domain.DomainEvent;
using Common.Domain.Exceptions;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.DomainEvents;

namespace Modules.Catalog.Application.UseCases.Categories.Projections;

public class CategorySpecCreatedDomainEventHandler(
    IDbConnectionFactory dbConnectionFactory,
    IServiceScopeFactory serviceScopeFactory
) : IDomainEventHandler<CategorySpecCreatedDomainEvent>
{
    public async Task Handle(CategorySpecCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        ICategorySpecRepositroy categorySpecRepositroy = scope.ServiceProvider.GetRequiredService<ICategorySpecRepositroy>();
        var categorySpecId = notification.CategorySpecId;
        var categorySpec = await categorySpecRepositroy.GetByIdAsync(categorySpecId);
        if (categorySpec is null)
        {
            throw new SkillHiveException($"Product with ID {categorySpecId} not found.");
        }
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
            .ExecuteAsync(UpdateQuery, new { categoryId = categorySpec.CategoryId });
    }

}
