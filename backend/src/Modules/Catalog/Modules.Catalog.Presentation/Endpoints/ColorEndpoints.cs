using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Catalog.Application.UseCases.Colors.CreateColor;
using Modules.Catalog.Application.UseCases.Colors.PaginateColors;
using Common.Application.Extensions;
using Common.Presentation.Endpoints;
using Modules.Catalog.Presentation;

namespace Modules.Catalog.Presentation.Endpoints;

public class ColorEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/colors").WithTags("Colors");

        group.MapPost("", async ([FromBody] CreateColorCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.ColorCreate);

        group.MapGet("", async (int? pageNumber, int? pageSize, string? colorName, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new PaginateColorsQuery(pageNumber ?? 1, pageSize ?? 50, colorName));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.ColorRead);
    }

}
