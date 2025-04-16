using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Presentation.Endpoints;
using Modules.Orders.Application.UseCases.CreateSpecOption;
using Microsoft.AspNetCore.Http;
using Modules.Common.Application.Extensions;
using MediatR;
using Modules.Orders.Application.UseCases.GetCategorySpecOption;

namespace Modules.Orders.Presentation.Endpoints;

public class SpecsEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/specs").WithTags("Specifications");

        group.MapPost("{id}", async (
            [FromRoute] Guid id,
            [FromBody] CreateCategorySpecOptionRequestDto request,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateSpecOptionCommand(id, request.value));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapGet("{id}", async (
            [FromRoute] Guid id,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetSpecOptionQuery(id));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });
    }
    public record CreateCategorySpecOptionRequestDto(string value);

}
