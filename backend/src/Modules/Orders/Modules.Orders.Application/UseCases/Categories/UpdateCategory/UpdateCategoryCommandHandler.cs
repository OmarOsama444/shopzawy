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
        var keyMix =
            request.Names.Keys
            .Union(request.Descriptions.Keys)
            .Union(request.ImageUrls.Keys);

        foreach (Language langCode in keyMix)
        {
            CategoryTranslation? categoryTranslation = await
                categoryTranslationRepository
                    .GetByIdAndLang(request.Id, langCode);
            if (categoryTranslation == null)
                return new CategoryTranslationNotFound(request.Id, langCode);
            else
            {
                categoryTranslation.Update(
                    request.Names.TryGetValue(langCode, out string? name) ? name : null,
                    request.Descriptions.TryGetValue(langCode, out string? description) ? description : null,
                    request.ImageUrls.TryGetValue(langCode, out string? imageUrl) ? imageUrl : null
                    );
                categoryTranslationRepository.Update(categoryTranslation);
            }
        }
        categoryRepository.Update(category);

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
