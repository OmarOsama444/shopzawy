using System.Data;
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Services;
using Modules.Orders.Application.UseCases.CreateProduct;


namespace Modules.Orders.Application.UseCases.ProductItems.CreateProductItem;

public record CreateProductItemCommand(
        Guid productId,
        ICollection<product_item> product_items
        ) : ICommand<ICollection<Guid>>;

public sealed class CreateProductItemCommandHandler(
    IProductService productService
) : ICommandHandler<CreateProductItemCommand, ICollection<Guid>>
{
    public async Task<Result<ICollection<Guid>>> Handle(CreateProductItemCommand request, CancellationToken cancellationToken)
    {
        var productItemIds = await productService.CreateProductItems(request.productId, request.product_items);
        return productItemIds;
    }
}

internal class CreateProductItemCommandValidator : AbstractValidator<CreateProductItemCommand>
{
    public CreateProductItemCommandValidator()
    {
        RuleFor(x => x.productId).NotEmpty();
        RuleForEach(x => x.product_items)
            .SetValidator(new productItemValidator());
    }
}
