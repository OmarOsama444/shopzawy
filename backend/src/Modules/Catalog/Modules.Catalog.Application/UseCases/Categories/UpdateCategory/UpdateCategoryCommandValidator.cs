
using Common.Application.Validators;
using FluentValidation;

namespace Modules.Catalog.Application.UseCases.Categories.UpdateCategory;

internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleForEach(x => x.Names.Values).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleForEach(x => x.ImageUrls.Values).NotEmpty().Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
        RuleForEach(x => x.Descriptions.Values).NotEmpty().MinimumLength(10).MaximumLength(500);
        RuleFor(x => x)
            .Must(x => ConsistentKeysValidator.Must(x.AddNames, x.AddDescriptions, x.AddImageUrls))
            .WithMessage(ConsistentKeysValidator.Message);
    }

}
