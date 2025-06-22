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
    IProductItemRepository productItemRepository,
    ISpecRepository specRepository,
    ISpecOptionRepository specOptionRepository,
    IProductItemOptionsRepository productItemOptionsRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateProductItemCommand, ICollection<Guid>>
{
    public async Task<Result<ICollection<Guid>>> Handle(CreateProductItemCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
            return new ProductNotFoundException(request.ProductId);
        var ProductItemIds = new List<Guid>();
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
            var specifications = await specRepository.GetByCategoryId(Language.en, product.CategoryId);
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
            ProductItemIds.Add(productItem.Id);
        }

        return ProductItemIds;
    }
}
