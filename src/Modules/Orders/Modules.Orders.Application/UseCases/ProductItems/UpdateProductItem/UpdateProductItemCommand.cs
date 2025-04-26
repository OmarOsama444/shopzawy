using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.ProductItems.UpdateProductItem;

public record UpdateProductItemCommand(
    Guid productItemId,
    string? stockKeepingUnit,
    int? quantityInStock,
    float? price,
    ICollection<string>? urls
) : ICommand<Guid>;

public sealed class UpdateProductItemCommandHandler(
    IProductItemRepository productItemRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateProductItemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateProductItemCommand request, CancellationToken cancellationToken)
    {
        var productItem = await productItemRepository.GetByIdAsync(request.productItemId);
        if (productItem == null)
        {
            return new ProductItemNotFoundException(request.productItemId);
        }
        productItem.Update(
            request.stockKeepingUnit,
            request.quantityInStock,
            request.price,
            request.urls);
        await unitOfWork.SaveChangesAsync();
        return request.productItemId;
    }
}

internal class UpdateProductItemCommandValidator : AbstractValidator<UpdateProductItemCommand>
{
    public UpdateProductItemCommandValidator()
    {
        RuleFor(x => x.productItemId).NotEmpty();
        RuleFor(x => x.quantityInStock).GreaterThan(0).When(x => x != null);
        RuleFor(x => x.price).GreaterThan(0).When(x => x != null);
        RuleForEach(x => x.urls)
        .Must(UrlValidator.Must)
        .WithMessage(UrlValidator.Message);
    }
}