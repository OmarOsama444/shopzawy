
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.UpdateCategory;

public record UpdateCategoryCommand(
    Guid id,
    int? order,
    IDictionary<Language, string> names,
    IDictionary<Language, string> descriptions,
    IDictionary<Language, string> imageUrls) : ICommand;

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
internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.id).NotEmpty();
        RuleFor(c => c.order).NotEmpty();
        RuleForEach(x => x.names.Values).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleForEach(x => x.imageUrls.Values).NotEmpty().Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
        RuleForEach(x => x.descriptions.Values).NotEmpty().MinimumLength(10).MaximumLength(500);
    }

}
