using System.Xml.Serialization;
using Common.Domain;
using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Application.Services;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Domain.Entities.Translation;
using Modules.Catalog.Domain.Entities.Views;
using Modules.Catalog.Domain.Exceptions;
using Modules.Catalog.Infrastructure.Data;
namespace Modules.Catalog.Infrastructure.Services;

public class CategoryService
(
    OrdersDbContext context,
    ICategoryRepository categoryRepository,
    ICategoryTranslationRepository categoryTranslationRepository,
    ICategoryStatisticsRepository categoryStatisticsRepository
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

        Category? parentCategory = null;
        if (parentCategoryId.HasValue)
        {
            parentCategory = await context.Categories.FirstOrDefaultAsync(x => x.Id == parentCategoryId.Value);
            if (parentCategory == null)
                return new CategoryNotFoundException(parentCategoryId.Value);
        }

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
            categoryTranslationRepository.Add(categoryTranslation);
        }
        categoryStatisticsRepository.Add(
            CategoryStatistics.Create(
                category.Id,
                parentCategoryId,
                [],
                0,
                0,
                0,
                category.Order,
                category.CreatedOn
            )
        );
        await context.SaveChangesAsync();
        return category.Id;
    }

}
