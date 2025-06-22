using FluentValidation;

namespace Modules.Orders.Application.UseCases.Products.UpdateProduct;

internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.product_id).NotEmpty();
        RuleFor(c => c.weight_unit)
            .NotEmpty()
            .When(x => x.weight_unit != null);
        RuleFor(c => c.dimension_unit)
            .NotEmpty()
            .When(x => x.dimension_unit != null);
    }
}