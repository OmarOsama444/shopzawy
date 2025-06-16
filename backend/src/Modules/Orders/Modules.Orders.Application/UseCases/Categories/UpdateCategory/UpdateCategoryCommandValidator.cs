
using FluentValidation;
using Modules.Common.Application.Validators;

namespace Modules.Orders.Application.UseCases.UpdateCategory;

internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.id).NotEmpty();
        RuleForEach(x => x.names.Values).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleForEach(x => x.imageUrls.Values).NotEmpty().Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
        RuleForEach(x => x.descriptions.Values).NotEmpty().MinimumLength(10).MaximumLength(500);
    }

}
