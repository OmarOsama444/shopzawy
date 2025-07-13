using Common.Application.Messaging;

namespace Modules.Catalog.Application.UseCases.ProductItems.DeleteProductItem;

public record DeleteProductItemCommand(Guid id) : ICommand<Guid>;
