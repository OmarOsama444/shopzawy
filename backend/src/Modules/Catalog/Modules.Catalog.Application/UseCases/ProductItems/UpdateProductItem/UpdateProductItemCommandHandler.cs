using Common.Application.Messaging;
using Common.Domain;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Exceptions;

namespace Modules.Catalog.Application.UseCases.ProductItems.UpdateProductItem;

public sealed class UpdateProductItemCommandHandler(
    IProductItemRepository productItemRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateProductItemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateProductItemCommand request, CancellationToken cancellationToken)
    {
        var productItem = await productItemRepository.GetByIdAsync(request.ProductItemId);
        if (productItem == null)
        {
            return new ProductItemNotFoundException(request.ProductItemId);
        }
        productItem.Update(
            request.StockKeepingUnit,
            request.QuantityInStock,
            request.Price,
            request.Width,
            request.Length,
            request.Height,
            request.Weight,
            request.AddUrls,
            request.RemoveUrls);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return request.ProductItemId;
    }
}
