using Common.Application.Messaging;
using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.UpdateCategory;

public sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    ICategoryTranslationRepository categoryTranslationRepository,
    ISpecRepository specRepository,
    ICategorySpecRepositroy categorySpecRepositroy
    ) : ICommandHandler<UpdateCategoryCommand>
{
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetByIdAsync(request.Id);
        if (category is null)
            return new CategoryNotFoundException(request.Id);
        category.Update(request.Order);

        foreach (Language langCode in request.Names.Keys.Union(request.Descriptions.Keys).Union(request.ImageUrls.Keys))
        {
            var categoryTranslation = await categoryTranslationRepository.GetByIdAndLang(request.Id, langCode);
            if (categoryTranslation == null)
                return new CategoryTranslationNotFound(request.Id, langCode);
            string? name = request.Names.TryGetValue(langCode, out var Name) ? Name : Name;
            string? description = request.Descriptions.TryGetValue(langCode, out var Description) ? Description : Description;
            string? imageUrl = request.ImageUrls.TryGetValue(langCode, out var ImageUrl) ? ImageUrl : ImageUrl;
            categoryTranslation.Update(name, description, imageUrl);
            categoryTranslationRepository.Update(categoryTranslation);
        }

        categoryRepository.Update(category);

        // Adding Translation
        foreach (var key in request.Names.Keys)
        {
            if (await categoryTranslationRepository.GetByIdAndLang(request.Id, key) is not null)
            {
                return new CategoryNameConflictException(request.Names[key]);
            }
            var categoryTranslation =
                CategoryTranslation
                .Create(request.Id, key, request.Names[key], request.Descriptions[key], request.ImageUrls[key]);
            categoryTranslationRepository.Add(categoryTranslation);
        }

        // Remove Translation 
        foreach (var key in request.RemoveTranslation)
        {
            var categoryTranslation =
                await categoryTranslationRepository.GetByIdAndLang(request.Id, key);
            if (categoryTranslation == null)
                continue;
            categoryTranslationRepository.Remove(categoryTranslation);
        }

        foreach (Guid specId in request.Add.Except(request.Remove).ToList())
        {
            var specification = await specRepository.GetByIdAsync(specId);
            if (specification is null)
                return new SpecificationNotFoundException(specId);
            if (await categorySpecRepositroy.GetByCategoryIdAndSpecId(request.Id, specId) is null)
            {
                var categorySpec = CategorySpec.Create(request.Id, specId);
                categorySpecRepositroy.Add(categorySpec);
            }
        }
        foreach (Guid specId in request.Remove)
        {
            var specification = await specRepository.GetByIdAsync(specId);
            if (specification is null)
                return new SpecificationNotFoundException(specId);
            var categorySpec =
                await
                    categorySpecRepositroy
                    .GetByCategoryIdAndSpecId(request.Id, specId);
            if (categorySpec is not null)
            {
                categorySpecRepositroy.Remove(categorySpec);
            }
        }
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
