using Microsoft.EntityFrameworkCore;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Services;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Services;

public class CategoryService
(
    OrdersDbContext context,
    ICategoryRepository categoryRepository,
    ICategorySpecRepositroy categorySpecRepositroy
) : ICategoryService
{
    public async Task<Result<string>> CreateCategory(
        string CategoryName,
        string Description,
        int Order,
        string ImageUrl,
        string? ParentCategoryName,
        ICollection<Guid> Ids)
    {

        if (await context.Categories.AnyAsync(x => x.CategoryName == CategoryName))
            return new CategoryNameConflictException(CategoryName);
        await context.Database.BeginTransactionAsync();
        try
        {
            Category? parentCategory = null;
            HashSet<Guid> specIds = new(Ids);
            var category = Category.Create(
                    CategoryName,
                    Description,
                    Order,
                    ImageUrl,
                    parentCategory
                );
            categoryRepository.Add(category);
            await context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(ParentCategoryName))
            {
                if (!await context.Categories.AnyAsync(x => x.CategoryName == ParentCategoryName))
                    return new CategoryNotFoundException(ParentCategoryName);

                HashSet<Guid> parentSpecIds = await context
                    .CategorySpecs
                    .Where(x => x.CategoryName == ParentCategoryName)
                    .Select(x => x.SpecId)
                    .ToHashSetAsync();

                specIds.ExceptWith(parentSpecIds);

                await context
                .CategorySpecs
                .Where(c => c.CategoryName == ParentCategoryName)
                .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.CategoryName, CategoryName));

                await context
                .Products
                .Where(p => p.CategoryName == ParentCategoryName)
                .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.CategoryName, CategoryName));

            }
            foreach (var id in specIds)
            {
                var categorySpec = CategorySpec.Create(CategoryName, id);
                categorySpecRepositroy.Add(categorySpec);
            }
            await context.SaveChangesAsync();
            await context.Database.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await context.Database.RollbackTransactionAsync();
            return ex;
        }
        return CategoryName;
    }


}
