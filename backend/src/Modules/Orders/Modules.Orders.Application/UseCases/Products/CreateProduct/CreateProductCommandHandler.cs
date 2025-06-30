using Common.Application.Messaging;
using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Products.CreateProduct;

public sealed class CreateProductCommandHandler(
    ICategoryRepository categoryRepository,
    IVendorRepository vendorRepository,
    IBrandRepository brandRepository,
    IProductItemRepository productItemRepository,
    IProductTranslationsRepository productTranslationsRepository,
    ISpecRepository specRepository,
    ISpecOptionRepository specOptionRepository,
    IProductItemOptionNumericRepository productItemOptionNumericRepository,
    IProductItemOptionColorRepository productItemOptionColorRepository,
    IColorRepository colorRepository,
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

        var specifications = await specRepository.GetByCategoryId(category.Id, [.. category.Path, category.Id], Language.en);
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

            foreach (var specOption in product_item.StringOptions)
            {
                if (specifications.Any(x => x.Id == specOption.Key && x.DataType == SpecDataType.String)
                    && await specOptionRepository.GetBySpecIdAndValue(specOption.Key, specOption.Value) != null)
                {
                    ProductItemOptions productItemOptions = ProductItemOptions.Create(
                        productItem.Id,
                        specOption.Key,
                        specOption.Value
                    );

                    productItemOptionsRepository.Add(productItemOptions);
                }
                else
                {
                    return new SpecificationNotFoundException(specOption.Key);
                }
            }

            foreach (var specOption in product_item.NumericOptions)
            {
                if (specifications.Any(x => x.Id == specOption.Key && x.DataType == SpecDataType.Number))
                {
                    ProductItemOptionNumeric productItemOptions = ProductItemOptionNumeric.Create(
                        productItem.Id,
                        specOption.Key,
                        specOption.Value
                    );

                    productItemOptionNumericRepository.Add(productItemOptions);
                }
                else
                {
                    return new SpecificationNotFoundException(specOption.Key);
                }
            }

            foreach (var specOption in product_item.ColorOptions)
            {
                if (await colorRepository.GetByIdAsync(specOption.Value) is null)
                {
                    return new NotFoundException("Color.NotFound", $"Color with code {specOption.Value} not found");
                }
                if (specifications.Any(x => x.Id == specOption.Key && x.DataType == SpecDataType.Color))
                {
                    ProductItemOptionColor productItemOptions = ProductItemOptionColor.Create(
                        productItem.Id,
                        specOption.Key,
                        specOption.Value
                    );

                    productItemOptionColorRepository.Add(productItemOptions);
                }
                else
                {
                    return new SpecificationNotFoundException(specOption.Key);
                }
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result<Guid>.Success(product.Id);
    }
}
