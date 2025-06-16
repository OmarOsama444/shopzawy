using Microsoft.EntityFrameworkCore;
using Modules.Common.Domain;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Application.Services;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Infrastructure.Data;
namespace Modules.Orders.Infrastructure.Services;

public class CategoryService
(
    OrdersDbContext context,
    ICategoryRepository categoryRepository
) : ICategoryService
{
    public async Task<Result<Guid>> CreateCategory(
        int Order,
        Guid? parentCategoryId,
        ICollection<Guid> Ids,
        IDictionary<Language, string> names,
        IDictionary<Language, string> descriptions,
        IDictionary<Language, string> imageUrls)
    {
        foreach (Language langCode in names.Keys)
        {
            if (await context.CategoryTranslations
                .AnyAsync(
                    x => x.LangCode == langCode
                    && x.Name == names[langCode])
                )
                return new CategoryNameConflictException(names[langCode]);
        }
        try
        {
            await context.Database.BeginTransactionAsync();
            Category? parentCategory = null;

            var category = Category.Create(
                    Order,
                    parentCategory
                );
            categoryRepository.Add(category);
            await context.SaveChangesAsync();
            foreach (Language langCode in names.Keys)
            {
                var categoryTranslation = CategoryTranslation.Create(
                    category.Id,
                    langCode,
                    names[langCode],
                    descriptions[langCode],
                    imageUrls[langCode]);
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
