using System.Globalization;
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.UpdateProduct;

public record UpdateProductCommand(
    Guid productId,
    string? productName,
    string? longDescription,
    string? shortDescription,
    string? imageUrl,
    WeightUnit? weightUnit,
    float? weight,
    float? price,
    DimensionUnit? dimensionUnit,
    float? width,
    float? length,
    float? height,
    ICollection<string>? tags) : ICommand<Guid>;

public sealed class UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.productId);
        if (product is null)
            return new ProductNotFoundException(request.productId);
        product.Update(
            request.productName,
            request.longDescription,
            request.shortDescription,
            request.weightUnit,
            request.weight,
            request.dimensionUnit,
            request.width,
            request.length,
            request.height,
            request.tags);
        await unitOfWork.SaveChangesAsync();
        return product.Id;
    }
}

internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.productId).NotEmpty();
        RuleFor(c => c.productName)
            .NotEmpty()
            .When(x => !string.IsNullOrEmpty(x.productName));
        RuleFor(c => c.longDescription)
            .MinimumLength(10)
            .When(x => !string.IsNullOrEmpty(x.longDescription));
        RuleFor(c => c.shortDescription)
            .MinimumLength(10)
            .When(x => !string.IsNullOrEmpty(x.shortDescription));
        RuleFor(c => c.weightUnit)
            .NotEmpty()
            .When(x => x.weightUnit != null);
        RuleFor(c => c.weight)
            .NotEmpty()
            .When(x => x.weight != null);
        RuleFor(c => c.dimensionUnit)
            .NotEmpty()
            .When(x => x.dimensionUnit != null);
        RuleFor(c => c.width)
            .NotEmpty()
            .When(x => x.width != null);
        RuleFor(c => c.height)
            .NotEmpty()
            .When(x => x.height != null);
        RuleFor(c => c.length)
            .NotEmpty()
            .When(x => x.length != null);
    }
}