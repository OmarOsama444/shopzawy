using System.Data;
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.ProductItems.CreateProductItem;

public record CreateProductItemCommand(
        string stockKeepingUnit,
        int quantityInStock,
        float price,
        Guid productId,
        ICollection<string> urls
        ) : ICommand<Guid>;

public sealed class CreateProductItemCommandHandler(
    IProductItemRepository productItemRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateProductItemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductItemCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.productId);
        ProductItem? productItem = productItemRepository.GetByProductIdAndSku(request.productId, request.stockKeepingUnit);
        if (product == null)
            return new ProductNotFoundException(request.productId);
        productItem = ProductItem.Create(
            request.stockKeepingUnit,
            request.quantityInStock,
            request.price,
            request.productId,
            request.urls);
        productItemRepository.Add(productItem);
        await unitOfWork.SaveChangesAsync();
        return productItem.Id;
    }
}

internal class CreateProductItemCommandValidator : AbstractValidator<CreateProductItemCommand>
{
    public CreateProductItemCommandValidator()
    {
        RuleFor(x => x.stockKeepingUnit).NotEmpty();
        RuleFor(x => x.quantityInStock).GreaterThan(0);
        RuleFor(x => x.price).GreaterThan(0);
        RuleFor(x => x.productId).NotEmpty();
        RuleFor(x => x.urls).NotEmpty();
        RuleForEach(x => x.urls)
            .NotEmpty()
            .WithMessage("Urls Can't be empty")
            .Must(UrlValidator.Must)
            .WithMessage(UrlValidator.Message);
    }
}
