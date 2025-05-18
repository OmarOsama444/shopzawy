using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Application.Extensions;
using Modules.Common.Presentation.Endpoints;
using Modules.Orders.Application.UseCases.Categories.CreateCategorySpec;
using Modules.Orders.Application.UseCases.Categories.GetCategory;
using Modules.Orders.Application.UseCases.CreateCategory;
using Modules.Orders.Application.UseCases.GetMainCategories;
using Modules.Orders.Application.UseCases.PaginateCategories;
using Modules.Orders.Application.UseCases.UpdateCategory;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Presentation.Endpoints;

public class CategoryEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/categories").WithTags("Categories");

        group.MapPost("", async ([FromBody] CreateCategoryCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapGet("main", async ([FromServices] ISender sender, [FromQuery] Language lang_code = Language.en) =>
        {
            var result = await sender.Send(new GetMainCategoriesQuery(lang_code));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapGet("", async (
            [FromServices] ISender sender,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 50,
            [FromQuery] Language lang_code = Language.en,
            [FromQuery] string? nameFilter = null) =>
        {
            var result = await sender.Send(new PaginateCategoryQuery(pageNumber, pageSize, nameFilter, lang_code));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapGet("{id}", async ([FromRoute] Guid id, [FromServices] ISender sender, [FromQuery] Language lang_code = Language.en) =>
        {
            var result = await sender.Send(new GetCategoryByIdQuery(id, lang_code));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapPut("{id}", async (
            [FromServices] ISender sender,
            [FromRoute] Guid id,
            [FromBody] UpdateCategoryRequestDto request) =>
        {
            var result = await sender.Send(new UpdateCategoryCommand(
                id,
                request.order,
                new(request?.names?.translations!),
                new(request?.descriptions?.translations!),
                new(request?.image_urls?.translations!)));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        });

        group.MapPost("{id}/specs/", async (
            [FromRoute] Guid id,
            [FromBody] CreateCategorySpecRequestDto request,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateCategorySpecCommand(id, request.Ids));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        });

    }

    public record LocalizedText(IDictionary<Language, string> translations);
    public record CreateCategorySpecRequestDto(ICollection<Guid> Ids);
    public record UpdateCategoryRequestDto(
        int? order,
        LocalizedText names,
        LocalizedText descriptions,
        LocalizedText image_urls);

}
