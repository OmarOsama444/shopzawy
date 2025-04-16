using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Presentation.Endpoints;
using Modules.Orders.Application.UseCases.CreateBrand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Modules.Common.Application.Extensions;
using Modules.Orders.Application.UseCases.PaginateBrands;
using Modules.Orders.Application.UseCases.UpdateBrand;

namespace Modules.Orders.Presentation.Endpoints;

public class BrandEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/brands").WithTags("Brands");
        group.MapPost("", async ([FromBody] CreateBrandCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapGet("", async ([FromServices] ISender sender, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? nameField = null) =>
        {
            var result = await sender.Send(new PaginateBrandsQuery(pageNumber, pageSize, nameField));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapPut("{name}", async ([FromRoute] string name, [FromBody] UpdateBrandRequestDto request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(
            new UpdateBrandCommand(name,
            request.LogoUrl,
            request.Description,
            request.Featured,
            request.Active));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        });
    }

    public record UpdateBrandRequestDto(
    string? LogoUrl,
    string? Description,
    bool? Featured,
    bool? Active);

}
