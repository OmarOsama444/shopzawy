using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.DomainEvents;
using Modules.Orders.Domain.Elastic;
using Modules.Orders.Domain.Exceptions;

namespace Modules.Orders.Application.UseCases.Products.Projections;

public class ProductCreatedDomainEventHandler(
    IOrdersDbContext context,
    ICategoryRepository categoryRepository,
    IProductDocumentRepository productDocumentRepository
) : IDomainEventHandler<ProductCreatedDomainEvent>
{
    public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var product = await context.Products.Include(x => x.ProductTranslations).FirstOrDefaultAsync(x => x.Id == notification.ProductId) ?? throw new ProductNotFoundException(notification.ProductId);
        var CategoryIds = (await categoryRepository.GetCategoryPath(product.CategoryId, Language.en)).Select(x => x.Key).ToList();
        var EnglishTranslation = product.ProductTranslations.FirstOrDefault(x => x.LangCode == Language.en);
        var ArabicTranslation = product.ProductTranslations.FirstOrDefault(x => x.LangCode == Language.ar);
        var productDocument = ProductDocument.Create(
            notification.ProductId,
            product.VendorId,
            product.BrandId,
            CategoryIds,
            LocalizedField.Create(EnglishTranslation?.ProductName, ArabicTranslation?.ProductName),
            LocalizedField.Create(EnglishTranslation?.LongDescription, ArabicTranslation?.LongDescription),
            LocalizedField.Create(EnglishTranslation?.ShortDescription, ArabicTranslation?.ShortDescription),
            []
            );
        await productDocumentRepository.Add(productDocument);
    }

}
