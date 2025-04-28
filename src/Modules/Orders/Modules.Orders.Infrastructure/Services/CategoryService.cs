using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Services;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Services;

public class CategoryService
(
    OrdersDbContext context,
    ICategoryRepository categoryRepository,
    ICategorySpecRepositroy categorySpecRepositroy
) : ICategoryService
{
    public async Task<Result<Guid>> CreateCategory(
        int Order,
        Guid? parentCategoryId,
        ICollection<Guid> Ids,
        IDictionary<Language, CategoryLangData> categoryLangPacks)
    {
        foreach (var categoryLangPack in categoryLangPacks)
        {
            if (await context.CategoryTranslations
                .AnyAsync(
                    x => x.LangCode == categoryLangPack.Key.GetDisplayName()
                    && x.Name == categoryLangPack.Value.name)
                )
                return new CategoryNameConflictException(categoryLangPack.Value.name);
        }

        try
        {
            await context.Database.BeginTransactionAsync();
            Category? parentCategory = null;
            HashSet<Guid> specIds = Ids.ToHashSet();
            var category = Category.Create(
                    Order,
                    parentCategory
                );
            categoryRepository.Add(category);
            await context.SaveChangesAsync();
            if (parentCategoryId != null)
            {
                if (!await context.Categories.AnyAsync(x => x.Id == parentCategoryId))
                    return new CategoryNotFoundException(parentCategoryId.Value);

                HashSet<Guid> parentSpecIds = await context
                    .CategorySpecs
                    .Where(x => x.Id == parentCategoryId.Value)
                    .Select(x => x.SpecId)
                    .ToHashSetAsync();

                specIds.ExceptWith(parentSpecIds);

                await context
                .CategorySpecs
                .Where(c => c.CategoryId == parentCategoryId.Value)
                .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.CategoryId, category.Id));

                await context
                .Products
                .Where(p => p.CategoryId == parentCategoryId)
                .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.CategoryId, category.Id));

            }
            foreach (var id in specIds)
            {
                var categorySpec = CategorySpec.Create(category.Id, id);
                categorySpecRepositroy.Add(categorySpec);
            }
            foreach (var categoryLangPack in categoryLangPacks)
            {
                var categoryTranslation = CategoryTranslation.Create(
                    category.Id,
                    categoryLangPack.Key.GetDisplayName(),
                    categoryLangPack.Value.name,
                    categoryLangPack.Value.description,
                    categoryLangPack.Value.image_url);
                context.CategoryTranslations.Add(categoryTranslation);
            }
            await context.SaveChangesAsync();
            await context.Database.CommitTransactionAsync();
            return category.Id;
        }
        catch (Exception ex)
        {
            await context.Database.RollbackTransactionAsync();
            return ex;
        }
    }


}
