using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Services;
using Modules.Orders.Application.UseCases.UpdateCategory;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.CreateCategory;

public record CategoryLangData(string? name, string? description, string? image_url);
public record LocalizedText(IDictionary<Language, string> translations);
public record CreateCategoryCommand(
    int order,
    Guid? parent_category_id,
    ICollection<Guid> spec_ids,
    LocalizedText names,
    LocalizedText descriptions,
    LocalizedText image_urls
) : ICommand<Guid>;
public class CreateCategoryCommandHandler(
    ICategoryService categoryService
    ) : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await categoryService.CreateCategory(
            request.order,
            request.parent_category_id,
            request.spec_ids,
            request.names.translations,
            request.descriptions.translations,
            request.image_urls.translations);
    }
}

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.order).NotEmpty();
        RuleForEach(x => x.names.translations.Values).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleForEach(x => x.image_urls.translations.Values).NotEmpty().Must(UrlValidator.Must!).WithMessage(UrlValidator.Message);
        RuleForEach(x => x.descriptions.translations.Values).NotEmpty().MinimumLength(10).MaximumLength(500);
        RuleFor(c => c)
            .Must(HaveConsistentLanguageKeys)
            .WithMessage("All localized fields must have the same set of language codes (keys).");

    }
    private bool HaveConsistentLanguageKeys(CreateCategoryCommand cmd)
    {
        var keys1 = cmd.names?.translations?.Keys?.OrderBy(k => k).ToArray();
        var keys2 = cmd.descriptions?.translations?.Keys?.OrderBy(k => k).ToArray();
        var keys3 = cmd.image_urls?.translations?.Keys?.OrderBy(k => k).ToArray();

        if (keys1 == null || keys2 == null || keys3 == null)
            return false;

        return keys1.SequenceEqual(keys2) && keys1.SequenceEqual(keys3);
    }
}
