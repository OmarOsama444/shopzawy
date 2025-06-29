using Common.Application.Messaging;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.DomainEvents;
using Modules.Orders.Domain.Elastic;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Products.Projections;

public class ProductItemCreatedDomainEventHandler(
    IOrdersDbContext context,
    IProductDocumentRepository productDocumentRepository) : IDomainEventHandler<ProductItemCreatedDomainEvent>
{
    public async Task Handle(ProductItemCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var productItem = await context.ProductItems
            .Include(x => x.ProductItemOptions)
            .ThenInclude(x => x.SpecificationOptions)
            .ThenInclude(x => x.Specification)
            .FirstOrDefaultAsync(x => x.Id == notification.ProductItemId) ?? throw new ProductItemNotFoundException(notification.ProductItemId) ?? throw new ProductItemNotFoundException(notification.ProductItemId);
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

            var productItemDocument = ProductItemDocument.Create(productItem.Id, productItem.Price, specNumberVariations, specStringVariations);
            await productDocumentRepository.AddProductItem(productItem.ProductId, productItemDocument);
        }
    }

}
