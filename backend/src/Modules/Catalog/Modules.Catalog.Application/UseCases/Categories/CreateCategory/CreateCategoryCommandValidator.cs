using Common.Application.Validators;
using FluentValidation;

namespace Modules.Catalog.Application.UseCases.Categories.CreateCategory;

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Order).NotEmpty();
        RuleFor(c => c.SpecIds).NotNull();
        RuleForEach(x => x.Names.Values).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleForEach(x => x.ImageUrls.Values).NotEmpty().Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
        RuleForEach(x => x.Descriptions.Values).NotEmpty().MinimumLength(3);
        RuleFor(c => c.Names).Must(LanguageValidator.Must).WithMessage(LanguageValidator.Message);
        RuleFor(c => c.Descriptions).Must(LanguageValidator.Must).WithMessage(LanguageValidator.Message);
        RuleFor(c => c.ImageUrls).Must(LanguageValidator.Must).WithMessage(LanguageValidator.Message);
        RuleForEach(x => x.Descriptions.Values).NotEmpty().MinimumLength(10).MaximumLength(500);
        RuleFor(c => c)
            .Must(x => ConsistentKeysValidator.Must(x.Names, x.Descriptions, x.ImageUrls))
            .WithMessage(ConsistentKeysValidator.Message);
    }

}
