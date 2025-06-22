using Common.Application.Messaging;
using Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Exceptions;

namespace Modules.Orders.Application.UseCases.ProductItems.DeleteProductItem;

public class DeleteProductItemCommandHandler(
    IProductItemRepository productItemRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<DeleteProductItemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(DeleteProductItemCommand request, CancellationToken cancellationToken)
    {
        var productItem = await productItemRepository.GetByIdAsync(request.id);
        if (productItem == null)
            return new ProductItemNotFoundException(request.id);
        productItemRepository.Remove(productItem);
        await unitOfWork.SaveChangesAsync();
        return request.id;
    }
}
