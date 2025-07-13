using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.DomainEvents;
using Modules.Catalog.Domain.Elastic;

namespace Modules.Catalog.Application.UseCases.Products.Projections;

public class ProductTranslationUpdatedDomainEventHandler(IServiceScopeFactory serviceScopeFactory) : IDomainEventHandler<ProductTranslationUpdatedDomainEvent>
{
    public async Task Handle(ProductTranslationUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        IOrdersDbContext ordersDbContext = scope.ServiceProvider.GetRequiredService<IOrdersDbContext>();
        IProductDocumentRepository productDocumentRepository = scope.ServiceProvider.GetRequiredService<IProductDocumentRepository>();
        var product = await ordersDbContext
        .Products
        .Include(x => x.Category)
        .Include(x => x.ProductTranslations)
        .Include(x => x.ProductItems)
            .ThenInclude(x => x.ProductItemOptions)
        .Include(x => x.ProductItems)
            .ThenInclude(x => x.ProductItemOptionNumerics)
        .Include(x => x.ProductItems)
            .ThenInclude(x => x.ProductItemOptionColors)
        .FirstOrDefaultAsync(x => x.Id == notification.ProductId, cancellationToken);
        var productItems = product!.ProductItems;
        List<Guid> CategoryIds = [.. product.Category.Path, product.Category.Id];
        var EnglishTranslation = product.ProductTranslations.FirstOrDefault(x => x.LangCode == Language.en);
        var ArabicTranslation = product.ProductTranslations.FirstOrDefault(x => x.LangCode == Language.ar);
        foreach (var productItem in productItems)
        {
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
                await productDocumentRepository.Update(productDocument);
            }
        }
    }
}