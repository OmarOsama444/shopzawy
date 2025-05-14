
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
    IDictionary<Language, CategoryLangData> category_lang_data) : ICommand;

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
        foreach (var langData in request.category_lang_data)
        {
            CategoryTranslation? categoryTranslation = await categoryTranslationRepository.GetByCategoryIdAndLangCode(request.id, langData.Key);
            CategoryLangData categoryLangData = langData.Value;
            if (categoryTranslation == null)
                return new CategoryTranslationNotFound(request.id, langData.Key);
            else
            {
                categoryTranslation.Update(categoryLangData.name, categoryLangData.description, categoryLangData.image_url);
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
        RuleForEach(c => c.category_lang_data)
            .SetValidator(new CategoryLangDataEntryValidator());
    }
}
public record CategoryLangData(string? name, string? description, string? image_url);
internal class CategoryLangDataEntryValidator : AbstractValidator<KeyValuePair<Language, CategoryLangData>>
{
    public CategoryLangDataEntryValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty();
        RuleFor(x => x.Value)
            .NotEmpty()
            .SetValidator(new CategoryLangDataValidator());
    }
}

internal class CategoryLangDataValidator : AbstractValidator<CategoryLangData>
{
    public CategoryLangDataValidator()
    {
        RuleFor(x => x.name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.image_url));
        RuleFor(x => x.image_url)
            .NotEmpty()
            .Must(UrlValidator.Must!)
            .WithMessage(UrlValidator.Message)
            .When(x => !string.IsNullOrEmpty(x.image_url));
        RuleFor(x => x.description)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(500)
            .When(x => !String.IsNullOrEmpty(x.image_url));
    }
}