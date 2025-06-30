using Common.Application.Messaging;
using Common.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.DomainEvents;

namespace Modules.Orders.Application.UseCases.Products.Projections;

public class ProductItemOutOfStockDomainEventHandler(
    IServiceScopeFactory serviceScopeFactory
) : IDomainEventHandler<ProductItemOutOfStockDomainEvent>
{
    public async Task Handle(ProductItemOutOfStockDomainEvent notification, CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        IOrdersDbContext context = scope.ServiceProvider.GetRequiredService<IOrdersDbContext>();
        IProductDocumentRepository productDocumentRepository = scope.ServiceProvider.

        GetRequiredService<IProductDocumentRepository>();
        var productItem = await context.ProductItems.FirstOrDefaultAsync(x => x.Id == notification.ProductItemId, cancellationToken) ?? throw new SkillHiveException($"Product Item with id {notification.ProductItemId} not found");

        if (productItem.QuantityInStock == 0)
            await productDocumentRepository.Delete(productItem.Id);
    }
}

