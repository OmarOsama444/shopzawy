using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using MediatR;
using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Application.UseCases.Specs.PaginateSpec;
using Modules.Orders.Application.UseCases.Specs.CreateSpec;
using Modules.Orders.Application.UseCases.Specs.UpdateSpec;
using Common.Application.Extensions;
using Common.Presentation.Endpoints;
using Common.Domain.ValueObjects;

namespace Modules.Orders.Presentation.Endpoints;

public class SpecsEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/specs").WithTags("Specifications");

        group.MapPut("{id}", async (
            [FromRoute] Guid id,
            [FromBody] UpdateSpecOptionRequestDto request,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new UpdateSpecCommand(id, request.SpecNames.translations, request.Add, request.Remove));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.SpecUpdate);

        group.MapPost("", async ([FromBody] CreateSpecRequestDto requestDto, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateSpecCommand(requestDto.spec_names, requestDto.dataType));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.SpecCreate);

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
        }).RequireAuthorization(Permissions.SpecRead);
    }
    public record CreateSpecRequestDto(IDictionary<Language, string> spec_names, SpecDataType dataType);
    public record UpdateSpecOptionRequestDto(
        LocalizedText SpecNames,
        ICollection<string> Add,
        ICollection<string> Remove);

}
