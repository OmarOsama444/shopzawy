using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.DomainEvents;
using Modules.Orders.Domain.Elastic;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Products.Projections;

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
        List<Guid> CategoryIds = [.. product.Category.Path, product.Category.Id];
        var EnglishTranslation = product.ProductTranslations.FirstOrDefault(x => x.LangCode == Language.en);
        var ArabicTranslation = product.ProductTranslations.FirstOrDefault(x => x.LangCode == Language.ar);
        if (productItem.QuantityInStock > 0)
        {
            List<Variation<string>> specStringVariations =
                [.. productItem
                .ProductItemOptions
                    .Select(x => Variation<string>
                    .Create(x.SpecificationId, x.Value)) ,
                ..
                productItem
                .ProductItemOptionColors
                    .Select(x => Variation<string>
                    .Create(x.SpecificationId , x.ColorCode ))];

            List<Variation<float>> specNumberVariations =
                [.. productItem
                .ProductItemOptionNumerics
                    .Select(x => Variation<float>
                    .Create(x.SpecificationId,x.Value))];
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

