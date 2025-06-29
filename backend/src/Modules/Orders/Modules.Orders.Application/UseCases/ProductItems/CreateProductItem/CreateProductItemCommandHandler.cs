using Common.Application.Messaging;
using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;


namespace Modules.Orders.Application.UseCases.ProductItems.CreateProductItem;

public sealed class CreateProductItemCommandHandler(
    IProductRepository productRepository,
    ISpecRepository specRepository,
    ICategoryRepository categoryRepository,
    ISpecOptionRepository specOptionRepository,
    IProductItemRepository productItemRepository,
    IProductItemOptionsRepository productItemOptionsRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateProductItemCommand, ICollection<Guid>>
{
    public async Task<Result<ICollection<Guid>>> Handle(CreateProductItemCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
            return new ProductNotFoundException(request.ProductId);
        var category = await categoryRepository.GetByIdAsync(product.CategoryId);
        if (category is null)
            return new CategoryNotFoundException(product.CategoryId);
        var ProductItemIds = new List<Guid>();
        foreach (var productItem in request.ProductItems)
        {
            ProductItem? ProductItem = await productItemRepository.GetByProductIdAndSku(request.ProductId, productItem.StockKeepingUnit);
            if (ProductItem is not null)
                return new ProductItemConflictException(productItem.StockKeepingUnit);
            ProductItem = ProductItem.Create(
               productItem.StockKeepingUnit,
               productItem.QuantityInStock,
               productItem.Price,
               productItem.Width,
               productItem.Length,
               productItem.Height,
               productItem.Weight,
               product.Id,
               productItem.Urls);
            productItemRepository.Add(ProductItem);
            var parentIds = (await categoryRepository.GetCategoryPath(product.CategoryId, Language.en)).Select(x => x.Key).ToArray();
            var specifications = (await specRepository.GetByCategoryId(category.Id, [.. category.Path, category.Id], Language.en)).Select(x => x.Id);
            foreach (var productSpec in productItem.SpecOptions)
            {
                if (
                    !specifications
                        .Contains(productSpec.Key)
                )
                    return new SpecificationNotFoundException(productSpec.Key);

                if ((await specOptionRepository.GetBySpecIdAndValue(productSpec.Key, productSpec.Value)) == null)
                    return new SpecificationOptionNotFoundException(productSpec.Key, productSpec.Value);

                ProductItemOptions productItemOptions = ProductItemOptions.Create(
                    ProductItem.Id,
                    productSpec.Key,
                    productSpec.Value
                );

                productItemOptionsRepository.Add(productItemOptions);

            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
            ProductItemIds.Add(ProductItem.Id);
        }
        return ProductItemIds;
    }
}
