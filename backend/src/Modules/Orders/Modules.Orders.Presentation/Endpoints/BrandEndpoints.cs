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
using Modules.Orders.Domain.ValueObjects;

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
        }).RequireAuthorization(Permissions.BrandCreate);

        group.MapGet("", async ([FromServices] ISender sender, [FromQuery] int page_number = 1, [FromQuery] int page_size = 10, [FromQuery] string? name_field = null, [FromQuery] Language lang_code = Language.en) =>
        {
            var result = await sender.Send(new PaginateBrandsQuery(page_number, page_size, name_field, lang_code));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.BrandRead);

        group.MapPut("{id}", async ([FromRoute] Guid id, [FromBody] UpdateBrandRequestDto request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(
            new UpdateBrandCommand(id,
            request.LogoUrl,
            request.Description,
            request.Featured,
            request.Active));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.BrandUpdate);
    }

    public record UpdateBrandRequestDto(
    string? LogoUrl,
    string? Description,
    bool? Featured,
    bool? Active);

}
