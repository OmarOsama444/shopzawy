using Common.Application.Messaging;

namespace Modules.Orders.Application.UseCases.ProductItems.DeleteProductItem;

public record DeleteProductItemCommand(Guid id) : ICommand<Guid>;
