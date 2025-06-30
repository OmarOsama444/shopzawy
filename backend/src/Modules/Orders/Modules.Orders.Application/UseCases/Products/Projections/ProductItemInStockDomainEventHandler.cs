using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.DomainEvents;
using Modules.Orders.Domain.Elastic;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Products.Projections;

public class ProductItemInStockDomainEventHandler(
    IOrdersDbContext context,
    ICategoryRepository categoryRepository,
    IProductDocumentRepository productDocumentRepository
) : IDomainEventHandler<ProductItemInStockDomainEvent>
{
    public async Task Handle(ProductItemInStockDomainEvent notification, CancellationToken cancellationToken)
    {
        var productItem = await context.ProductItems
          .Include(x => x.Product)
          .ThenInclude(x => x.ProductTranslations)
          .Include(x => x.ProductItemOptions)
          .ThenInclude(x => x.SpecificationOptions)
          .ThenInclude(x => x.Specification)
          .FirstOrDefaultAsync(x => x.Id == notification.ProductItemId, cancellationToken: cancellationToken) ??
       throw new ProductItemNotFoundException(notification.ProductItemId);
        var product = productItem.Product ?? throw new ProductNotFoundException(productItem.ProductId);
        var CategoryIds = (await categoryRepository.GetCategoryPath(product.CategoryId, Language.en)).Select(x => x.Key).ToList();
        var EnglishTranslation = product.ProductTranslations.FirstOrDefault(x => x.LangCode == Language.en);
        var ArabicTranslation = product.ProductTranslations.FirstOrDefault(x => x.LangCode == Language.ar);
        if (productItem.QuantityInStock > 0)
        {
            List<Variation<string>> specStringVariations =
                [.. productItem
                .ProductItemOptions
                    .Select(x => x.SpecificationOptions)
                    .Where(x => x.Specification.DataType != SpecDataType.Number)
                    .Select(x => Variation<string>
                    .Create(x.SpecificationId, x.Value))];

            List<Variation<float>> specNumberVariations =
                [.. productItem
                .ProductItemOptions
                    .Select(x => x.SpecificationOptions)
                    .Where(x => x.Specification.DataType == SpecDataType.Number)
                    .Select(x => Variation<float>
                    .Create(x.SpecificationId, float.Parse( x.Value ) ))];
            var productDocument = ProductDocument.Create(
                productItem.Id,
                product.Id,
                product.VendorId,
                product.BrandId,
                CategoryIds,
                LocalizedField.Create(EnglishTranslation?.ProductName, ArabicTranslation?.ProductName),
                LocalizedField.Create(EnglishTranslation?.LongDescription, ArabicTranslation?.LongDescription),
                LocalizedField.Create(EnglishTranslation?.ShortDescription, ArabicTranslation?.ShortDescription),
                productItem.Price,
                specNumberVariations,
                specStringVariations
                );
            await productDocumentRepository.Add(productDocument);
        }
    }
}

