using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Presentation.Endpoints;
using Modules.Orders.Application.UseCases.CreateProduct;
using MediatR;
using Modules.Common.Application.Extensions;
using Modules.Orders.Application.UseCases.ProductItems.CreateProductItem;

namespace Modules.Orders.Presentation.Endpoints;

public class ProductEndpoints : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products").WithTags("Products");
        group.MapPost("", async ([FromBody] CreateProductCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return
                result.isSuccess ?
                Results.Ok(result.Value) :
                result.ExceptionToResult();
        });

        group.MapPost("{id}",
            async ([FromRoute] Guid id, [FromBody] ProductItemRequest request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateProductItemCommand(
                request.stockKeepingUnit,
                request.quantityInStock,
                request.price,
                id,
                request.urls));
            return result.isSuccess ?
                Results.Ok(result.Value) :
                result.ExceptionToResult();
        });

        group.MapPut("{id}/items/{itemId}",
            async ([FromRoute] Guid id, [FromRoute] Guid productItemId, [FromBody] ProductItemRequest request, [FromServices] ISender sender) =>
        {
            // var result = await sender.Send();
        });
    }

}

public record ProductItemRequest(
    string stockKeepingUnit,
    int quantityInStock,
    float price,
    ICollection<string> urls);