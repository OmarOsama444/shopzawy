using System.Data;
using Common.Application.Messaging;
using Modules.Orders.Application.Services.Dtos;


namespace Modules.Orders.Application.UseCases.ProductItems.CreateProductItem;

public record CreateProductItemCommand(
        Guid ProductId,
        ICollection<ProductItemDto> ProductItems
        ) : ICommand<ICollection<Guid>>;
