using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.ProductItems.DeleteProductItem;

public record DeleteProductItemCommand(Guid id) : ICommand<Guid>;

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

internal class DeleteProductItemCommandValidator : AbstractValidator<DeleteProductItemCommand>
{
    public DeleteProductItemCommandValidator()
    {
        RuleFor(x => x.id);
    }
}