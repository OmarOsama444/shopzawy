using FluentValidation;

namespace Modules.Catalog.Application.UseCases.Products.UpdateProduct;

internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.WeightUnit)
            .NotEmpty()
            .When(x => x.WeightUnit != null);
        RuleFor(c => c.DimensionUnit)
            .NotEmpty()
            .When(x => x.DimensionUnit != null);
    }
}