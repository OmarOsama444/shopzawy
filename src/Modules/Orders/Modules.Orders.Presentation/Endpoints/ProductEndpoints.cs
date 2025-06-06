using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Presentation.Endpoints;
using Modules.Orders.Application.UseCases.CreateProduct;
using MediatR;
using Modules.Common.Application.Extensions;
using Modules.Orders.Application.Services;
using Modules.Orders.Application.UseCases.ProductItems.CreateProductItem;
using Modules.Orders.Application.UseCases.ProductItems.UpdateProductItem;
using Modules.Orders.Application.UseCases.ProductItems.DeleteProductItem;
using Modules.Orders.Domain.ValueObjects;


namespace Modules.Orders.Presentation.Endpoints;

public class ProductEndpoints : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products").WithTags("Products");
        group.MapPost("", async ([FromBody] ProductCreateRequest request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateProductCommand(
                request.product_names.translations,
                request.long_descriptions.translations,
                request.short_descriptions.translations,
                request.tags,
                request.weight_unit,
                request.dimension_unit,
                request.vendor_id,
                request.brand_id,
                request.category_id,
                request.product_items
            ));
            return
                result.isSuccess ?
                Results.Ok(result.Value) :
                result.ExceptionToResult();
        });

        group.MapPost("{Id}/items",
            async ([FromRoute] Guid Id, [FromBody] ICollection<product_item> request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateProductItemCommand(
                Id,
                request)
                );
            return result.isSuccess ?
                Results.Ok(result.Value) :
                result.ExceptionToResult();
        });

        group.MapPut("items/{Id}",
            async ([FromRoute] Guid Id, [FromBody] ProductItemRequest request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new UpdateProductItemCommand(
                    Id,
                    request.stock_keeping_unit,
                    request.quantity_in_stock,
                    request.price,
                    request.width,
                    request.length,
                    request.height,
                    request.weight,
                    request.image_urls));
        });

        group.MapDelete("items/{Id}",
            async ([FromRoute] Guid Id, [FromServices] ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductItemCommand(Id));
                return
                    result.isSuccess ?
                    Results.Ok(result.Value) :
                    result.ExceptionToResult();
            });
    }

}
public record ProductCreateRequest(
    LocalizedText product_names,
    LocalizedText long_descriptions,
    LocalizedText short_descriptions,
    ICollection<string> tags,
    WeightUnit weight_unit,
    DimensionUnit dimension_unit,
    Guid vendor_id,
    Guid brand_id,
    Guid category_id,
    ICollection<product_item> product_items);
public record ProductItemRequest(
    string? stock_keeping_unit,
    int? quantity_in_stock,
    float? price,
    float? width,
    float? length,
    float? height,
    float? weight,
    ICollection<string>? image_urls);