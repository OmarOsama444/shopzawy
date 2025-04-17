using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Presentation.Endpoints;
using Modules.Orders.Application.UseCases.CreateProduct;
using MediatR;
using Modules.Common.Application.Extensions;

namespace Modules.Orders.Presentation.Endpoints;

public class ProductEndpoints : IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products").WithTags("Products");
        group.MapPost("", async ([FromBody] CreateProductCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).WithName("Create Product")
        .WithDescription(
        """
        Body : 
        ```json
        {
        "productName": "string",
        "longDescription": "string",
        "shortDescription": "string",
        "imageUrl": "string",
        "weightUnit": Kilogram ,  // valid options : Gram,Kilogram,Pound,Ounce,Milligram,Ton 
        "weight": float ,
        "price": float, 
        "dimensionUnit": Centimeter , // valid options : Centimeter,Meter,Inch,Foot
        "width": float, 
        "length": float, 
        "height": float,
        "tags": [
            "Fashion" , "Bags" ..etc // add all the tags here
        ],
        "vendorId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", // guid
        "brandName": "string",
        "categoryName": "string"
        }
        ```
        """).WithOpenApi();
    }

}
