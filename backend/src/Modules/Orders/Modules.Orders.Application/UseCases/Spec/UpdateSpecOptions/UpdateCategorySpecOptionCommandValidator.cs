using FluentValidation;

namespace Modules.Orders.Application.UseCases.Spec.CreateSpecOption;

internal class UpdateCategorySpecOptionCommandValidator : AbstractValidator<UpdateSpecOptionsCommand>
{
    public UpdateCategorySpecOptionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}