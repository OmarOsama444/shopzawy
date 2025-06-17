using Common.Application.Messaging;
using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;

namespace Modules.Orders.Application.UseCases.UpdateCategory;

public sealed class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    ICategoryTranslationRepository categoryTranslationRepository
    ) : ICommandHandler<UpdateCategoryCommand>
{
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetByIdAsync(request.id);
        if (category is null)
            return new CategoryNotFoundException(request.id);
        category.Update(request.order);
        var keyMix =
            request.names.Keys
            .Union(request.descriptions.Keys)
            .Union(request.imageUrls.Keys);

        foreach (Language langCode in keyMix)
        {
            CategoryTranslation? categoryTranslation = await
                categoryTranslationRepository
                    .GetByIdAndLang(request.id, langCode);
            if (categoryTranslation == null)
                return new CategoryTranslationNotFound(request.id, langCode);
            else
            {
                categoryTranslation.Update(
                    request.names.TryGetValue(langCode, out string? name) ? name : null,
                    request.descriptions.TryGetValue(langCode, out string? description) ? description : null,
                    request.imageUrls.TryGetValue(langCode, out string? imageUrl) ? imageUrl : null
                    );
                categoryTranslationRepository.Update(categoryTranslation);
            }
        }
        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
