using FluentValidation;

namespace Modules.Orders.Application.UseCases.Categories.UpdateCategorySpec;

internal class UpdateCategorySpecCommandValidator : AbstractValidator<UpdateCategorySpecCommand>
{
    public UpdateCategorySpecCommandValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}