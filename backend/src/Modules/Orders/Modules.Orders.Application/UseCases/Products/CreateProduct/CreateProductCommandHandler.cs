using Common.Application.Messaging;
using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;

namespace Modules.Orders.Application.UseCases.Products.CreateProduct;

public sealed class CreateProductCommandHandler(
    ICategoryRepository categoryRepository,
    IVendorRepository vendorRepository,
    IBrandRepository brandRepository,
    IProductItemRepository productItemRepository,
    IProductTranslationsRepository productTranslationsRepository,
    ISpecRepository specRepository,
    ISpecOptionRepository specOptionRepository,
    IProductItemOptionsRepository productItemOptionsRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        Category? category = await categoryRepository.GetByIdAsync(request.CategoryId);

        if (category is null)
            return new CategoryNotFoundException(request.CategoryId);
        if (await vendorRepository.GetByIdAsync(request.VendorId) == null)
            return new VendorNotFoundException(request.VendorId);
        if (await brandRepository.GetByIdAsync(request.BrandId) == null)
            return new BrandNotFoundException(request.BrandId);

        var product = Product.Create(
            request.WeightUnit,
            request.DimensionUnit,
            request.Tags,
            request.VendorId,
            request.BrandId,
            request.CategoryId
        );

        productRepository.Add(product);
        foreach (Language langCode in request.ProductNames.Keys)
        {
            ProductTranslation productTranslation = ProductTranslation.Create(
                productId: product.Id,
                langCode: langCode,
                productName: request.ProductNames[langCode],
                longDescription: request.LongDescriptions[langCode],
                shortDescription: request.ShortDescriptions[langCode]);

            productTranslationsRepository.Add(productTranslation);
        }

        foreach (var product_item in request.ProductItems)
        {
            ProductItem productItem = ProductItem.Create(
                product_item.StockKeepingUnit,
                product_item.QuantityInStock,
                product_item.Price,
                product_item.Width,
                product_item.Length,
                product_item.Height,
                product_item.Weight,
                product.Id,
                product_item.Urls);
            productItemRepository.Add(productItem);
            var specifications = await specRepository.GetByCategoryId(Language.en, request.CategoryId);
            foreach (var specification in specifications)
            {
                if (product_item.SpecOptions.ContainsKey(specification.Id)
                    && specOptionRepository.GetBySpecIdAndValue(specification.Id, product_item.SpecOptions[specification.Id]) != null)
                {
                    ProductItemOptions productItemOptions = ProductItemOptions.Create(
                        productItem.Id,
                        specification.Id,
                        product_item.SpecOptions[specification.Id]
                    );

                    productItemOptionsRepository.Add(productItemOptions);
                }
                else
                {
                    return new SpecificationNotFoundException(specification.Id);
                }
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result<Guid>.Success(product.Id);
    }
}
