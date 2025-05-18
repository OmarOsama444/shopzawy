
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

public record LocalizedText(IDictionary<Language, string?> translations);
public record UpdateCategoryCommand(
    Guid id,
    int? order,
    LocalizedText names,
    LocalizedText descriptions,
    LocalizedText image_urls) : ICommand;

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
            request.names.translations.Keys
            .Union(request.descriptions.translations.Keys)
            .Union(request.image_urls.translations.Keys);

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
                    request.names.translations.TryGetValue(langCode, out string? name) ? name : null,
                    request.descriptions.translations.TryGetValue(langCode, out string? description) ? description : null,
                    request.image_urls.translations.TryGetValue(langCode, out string? imageUrl) ? imageUrl : null
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
        RuleForEach(x => x.names.translations.Values).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleForEach(x => x.image_urls.translations.Values).NotEmpty().Must(UrlValidator.Must!).WithMessage(UrlValidator.Message);
        RuleForEach(x => x.descriptions.translations.Values).NotEmpty().MinimumLength(10).MaximumLength(500);
    }

}
