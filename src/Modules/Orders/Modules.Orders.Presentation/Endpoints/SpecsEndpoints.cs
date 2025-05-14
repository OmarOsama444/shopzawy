using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Presentation.Endpoints;
using Modules.Orders.Application.UseCases.CreateSpecOption;
using Microsoft.AspNetCore.Http;
using Modules.Common.Application.Extensions;
using MediatR;
using Modules.Orders.Application.UseCases.GetCategorySpecOption;
using Modules.Orders.Application.UseCases;
using Modules.Orders.Application.UseCases.Spec.PaginateSpec;
using Modules.Orders.Domain.ValueObjects;

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
            var result = await sender.Send(new CreateSpecOptionsCommand(id, request.values));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapGet("{id}", async (
            [FromRoute] Guid id,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetSpecOptionQuery(id));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapPost("", async ([FromBody] CreateSpecRequestDto requestDto, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateSpecCommand(requestDto.spec_names, requestDto.dataType));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapGet("", async (
            [FromServices] ISender sender,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 50,
            [FromQuery] string? name = null,
            [FromQuery] Language lang_code = Language.en
            ) =>
        {
            var result = await sender.Send(new PaginateSpecQuery(pageNumber, pageSize, name, lang_code));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });
    }
    public record CreateSpecRequestDto(IDictionary<Language, string> spec_names, string dataType);
    public record CreateCategorySpecOptionRequestDto(ICollection<string> values);

}
