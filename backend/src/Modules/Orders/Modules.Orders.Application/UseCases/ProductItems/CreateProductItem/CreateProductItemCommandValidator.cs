using FluentValidation;
using Modules.Orders.Application.UseCases.Products.CreateProduct;


namespace Modules.Orders.Application.UseCases.ProductItems.CreateProductItem;

internal class CreateProductItemCommandValidator : AbstractValidator<CreateProductItemCommand>
{
    public CreateProductItemCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleForEach(x => x.ProductItems)
            .SetValidator(new productItemValidator());
    }
}
