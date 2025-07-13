using FluentValidation;

namespace Modules.Catalog.Application.UseCases.ProductItems.DeleteProductItem;

internal class DeleteProductItemCommandValidator : AbstractValidator<DeleteProductItemCommand>
{
    public DeleteProductItemCommandValidator()
    {
        RuleFor(x => x.id);
    }
}