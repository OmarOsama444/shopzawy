using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.DomainEvents;
using Modules.Catalog.Domain.Elastic;
using Modules.Catalog.Domain.Exceptions;

namespace Modules.Catalog.Application.UseCases.Products.Projections;

public class ProductItemInStockDomainEventHandler(
    IServiceScopeFactory serviceScopeFactory
) : IDomainEventHandler<ProductItemInStockDomainEvent>
{
    public async Task Handle(ProductItemInStockDomainEvent notification, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<IOrdersDbContext>();
        var categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();
        var productDocumentRepository = scope.ServiceProvider.GetRequiredService<IProductDocumentRepository>();

        var productItem = await context.ProductItems
          .Include(x => x.Product)
          .ThenInclude(x => x.ProductTranslations)
          .Include(x => x.Product)
          .ThenInclude(x => x.Category)
          .Include(x => x.ProductItemOptionColors)
          .Include(x => x.ProductItemOptionNumerics)
          .Include(x => x.ProductItemOptions)
          .FirstOrDefaultAsync(x => x.Id == notification.ProductItemId, cancellationToken: cancellationToken) ??
       throw new ProductItemNotFoundException(notification.ProductItemId);
        var product = productItem.Product ?? throw new ProductNotFoundException(productItem.ProductId);
        List<int> CategoryIds = [.. product.Category.Path, product.Category.Id];
        var EnglishTranslation = product.ProductTranslations.FirstOrDefault(x => x.LangCode == Language.en);
        var ArabicTranslation = product.ProductTranslations.FirstOrDefault(x => x.LangCode == Language.ar);
        if (productItem.QuantityInStock > 0)
        {
            List<StringVariation> specStringVariations =
                [.. productItem
                .ProductItemOptions
                    .Select(x => StringVariation
                    .Create(x.SpecificationId, x.Value)) ];

            List<NumericVariation> specNumberVariations =
                [.. productItem
                .ProductItemOptionNumerics
                    .Select(x => NumericVariation
                    .Create(x.SpecificationId,x.Value))];

            List<StringVariation> specColorVariations =
               [.. productItem
                .ProductItemOptionColors
                    .Select(x => StringVariation
                    .Create(x.SpecificationId, x.ColorCode)) ];

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
                specStringVariations,
                specColorVariations
                );
            await productDocumentRepository.Add(productDocument);
        }
    }
}

