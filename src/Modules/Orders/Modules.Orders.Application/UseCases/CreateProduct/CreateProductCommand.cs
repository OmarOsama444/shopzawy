using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.CreateProduct;

public record CreateProductCommand(
    string productName,
    string longDescription,
    string shortDescription,
    string imageUrl,
    WeightUnit weightUnit,
    float weight,
    float price,
    DimensionUnit dimensionUnit,
    float width,
    float length,
    float height,
    ICollection<string> tags,
    Guid vendorId,
    string brandName,
    string categoryName) : ICommand<Guid>;

public sealed class CreateProductCommandHandler(
    IVendorRepository vendorRepository,
    IBrandRepository brandRepository,
    ICategoryRepository categoryRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (await vendorRepository.GetByIdAsync(request.vendorId) is null)
            return new VendorNotFoundException(request.vendorId);
        if (await categoryRepository.GetByIdAsync(request.categoryName) is null)
            return new CategoryNotFoundException(request.categoryName);
        if (await brandRepository.GetByIdAsync(request.brandName) is null)
            return new BrandNotFoundException(request.brandName);

        var product = Product.Create(
            request.productName,
            request.longDescription,
            request.shortDescription,
            request.imageUrl,
            request.weightUnit,
            request.weight,
            request.price,
            request.dimensionUnit,
            request.weight,
            request.length,
            request.height,
            request.tags,
            request.vendorId,
            request.brandName,
            request.categoryName);

        productRepository.Add(product);
        await unitOfWork.SaveChangesAsync();
        return product.Id;
    }
}

internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.productName).NotEmpty();
        RuleFor(c => c.longDescription).NotEmpty().MinimumLength(10);
        RuleFor(c => c.shortDescription).NotEmpty().MinimumLength(10);
        RuleFor(c => c.imageUrl).Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
        RuleFor(c => c.weightUnit).NotEmpty();
        RuleFor(c => c.weight).NotEmpty();
        RuleFor(c => c.price).NotEmpty().GreaterThan(0);
        RuleFor(c => c.dimensionUnit).NotEmpty();
        RuleFor(c => c.width).NotEmpty();
        RuleFor(c => c.height).NotEmpty();
        RuleFor(c => c.length).NotEmpty();
    }
}
