using Common.Application.Validators;
using FluentValidation;

namespace Modules.Orders.Application.UseCases.Categories.CreateCategory;

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Order).NotEmpty();
        RuleFor(c => c.Spec_ids).NotNull();
        RuleForEach(x => x.Names.Values).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleForEach(x => x.Image_urls.Values).NotEmpty().Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
        RuleFor(c => c.Names).Must(LanguageValidator.Must).WithMessage(LanguageValidator.Message);
        RuleFor(c => c.Descriptions).Must(LanguageValidator.Must).WithMessage(LanguageValidator.Message);
        RuleFor(c => c.Image_urls).Must(LanguageValidator.Must).WithMessage(LanguageValidator.Message);
        RuleForEach(x => x.Descriptions.Values).NotEmpty().MinimumLength(10).MaximumLength(500);
        RuleFor(c => c)
            .Must(HaveConsistentLanguageKeys)
            .WithMessage("All localized fields must have the same set of language codes (keys).");

    }
    private bool HaveConsistentLanguageKeys(CreateCategoryCommand cmd)
    {
        var keys1 = cmd.Names?.Keys?.OrderBy(k => k).ToArray();
        var keys2 = cmd.Descriptions?.Keys?.OrderBy(k => k).ToArray();
        var keys3 = cmd.Image_urls?.Keys?.OrderBy(k => k).ToArray();

        if (keys1 == null || keys2 == null || keys3 == null)
            return false;

        return keys1.SequenceEqual(keys2) && keys1.SequenceEqual(keys3);
    }
}
