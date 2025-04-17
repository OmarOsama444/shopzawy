using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Application.Extensions;
using Modules.Common.Presentation.Endpoints;
using Modules.Orders.Application.UseCases.GetColors;
using Modules.Orders.Application.UseCases.CreateColor;

namespace Modules.Orders.Presentation.Endpoints;

public class ColorEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/colors").WithTags("Colors");

        group.MapGet("", async (int? pageNumber, int? pageSize, string? colorName, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new PaginateColorsQuery(pageNumber ?? 1, pageSize ?? 50, colorName));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });


        group.MapPost("", async ([FromBody] CreateColorCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        });
    }

}
