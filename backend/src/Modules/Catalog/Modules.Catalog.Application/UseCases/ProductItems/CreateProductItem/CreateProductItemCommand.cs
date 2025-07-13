using System.Data;
using Common.Application.Messaging;
using Modules.Catalog.Application.Services.Dtos;

namespace Modules.Catalog.Application.UseCases.ProductItems.CreateProductItem;

public record CreateProductItemCommand(
        Guid ProductId,
        ICollection<ProductItemDto> ProductItems
        ) : ICommand<ICollection<Guid>>;
