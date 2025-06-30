using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Orders.Application.UseCases.ProductItems.CreateProductItem;
using Modules.Orders.Application.UseCases.ProductItems.UpdateProductItem;
using Modules.Orders.Application.UseCases.ProductItems.DeleteProductItem;
using Modules.Orders.Domain.ValueObjects;
using Common.Application.Extensions;
using Common.Presentation.Endpoints;
using Modules.Orders.Application.Services.Dtos;
using Modules.Orders.Application.UseCases.Products.CreateProduct;
using Modules.Orders.Application.UseCases.Products.PaginateProducts;


namespace Modules.Orders.Presentation.Endpoints;

public class ProductEndpoints : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products").WithTags("Products");

        group.MapPost("filter", async ([FromBody] PaginateProductQuery request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ?
                Results.Ok(result.Value) :
                result.ExceptionToResult();
        }).AllowAnonymous();

        group.MapPost("", async ([FromBody] ProductCreateRequest request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateProductCommand(
                request.ProductNames.translations,
                request.LongDescriptions.translations,
                request.ShortDescriptions.translations,
                request.Tags,
                request.WeightUnit,
                request.DimensionUnit,
                request.VendorId,
                request.BrandId,
                request.CategoryId,
                request.ProductItems
            ));
            return
                result.isSuccess ?
                Results.Ok(result.Value) :
                result.ExceptionToResult();
        }).RequireAuthorization(Permissions.ProductCreate);

        group.MapPost("{Id}/items",
            async ([FromRoute] Guid Id, [FromBody] ICollection<ProductItemDto> request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateProductItemCommand(
                Id,
                request)
                );
            return result.isSuccess ?
                Results.Ok(result.Value) :
                result.ExceptionToResult();
        }).RequireAuthorization(Permissions.ProductItemCreate);

        group.MapPut("items/{Id}",
            async ([FromRoute] Guid Id, [FromBody] ProductItemRequest request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new UpdateProductItemCommand(
                    Id,
                    request.StockKeepingUnit,
                    request.QuantityInStock,
                    request.Price,
                    request.Width,
                    request.Length,
                    request.Height,
                    request.Weight,
                    request.AddImageUrls,
                    request.RemoveImageUrls));
        }).RequireAuthorization(Permissions.ProductItemUpdate);

        group.MapDelete("items/{Id}",
            async ([FromRoute] Guid Id, [FromServices] ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductItemCommand(Id));
                return
                    result.isSuccess ?
                    Results.Ok(result.Value) :
                    result.ExceptionToResult();
            })
            .RequireAuthorization(Permissions.ProductItemDelete);

    }

}
public record ProductCreateRequest(
    LocalizedText ProductNames,
    LocalizedText LongDescriptions,
    LocalizedText ShortDescriptions,
    List<string> Tags,
    WeightUnit WeightUnit,
    DimensionUnit DimensionUnit,
    Guid VendorId,
    Guid BrandId,
    Guid CategoryId,
    ICollection<ProductItemDto> ProductItems);
public record ProductItemRequest(
    string? StockKeepingUnit,
    int? QuantityInStock,
    float? Price,
    float? Width,
    float? Length,
    float? Height,
    float? Weight,
    ICollection<string>? AddImageUrls,
    ICollection<string>? RemoveImageUrls);