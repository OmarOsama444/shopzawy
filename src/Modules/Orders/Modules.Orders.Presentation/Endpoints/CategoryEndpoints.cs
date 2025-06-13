using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Application.Extensions;
using Modules.Common.Presentation.Endpoints;
using Modules.Orders.Application.UseCases.Categories.CreateCategory;
using Modules.Orders.Application.UseCases.Categories.GetCategory;
using Modules.Orders.Application.UseCases.Categories.UpdateCategorySpec;
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

        group.MapPost("", async ([FromBody] CreateCategoryRequestDto request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateCategoryCommand(
                request.Order,
                request.ParentCategoryId ?? Guid.Empty,
                request.SpecIds,
                request.Names.translations,
                request.Descriptions.translations,
                request.ImageUrls.translations
            ));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.CategoryCreate);

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
        }).RequireAuthorization(Permissions.CategoryRead);

        group.MapGet("{id}", async ([FromRoute] Guid id, [FromServices] ISender sender, [FromQuery] Language lang_code = Language.en) =>
        {
            var result = await sender.Send(new GetCategoryByIdQuery(id, lang_code));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.CategoryRead);

        group.MapPut("{id}", async (
            [FromServices] ISender sender,
            [FromRoute] Guid id,
            [FromBody] UpdateCategoryRequestDto request) =>
        {
            var result = await sender.Send(new UpdateCategoryCommand(
                id,
                request.Order,
                request.Names?.translations ?? new Dictionary<Language, string>(),
                request.Descriptions?.translations ?? new Dictionary<Language, string>(),
                request.ImageUrls?.translations ?? new Dictionary<Language, string>()));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.CategoryUpdate);

        group.MapPut("{id}/specs/", async (
            [FromRoute] Guid Id,
            [FromBody] UpdateCategorySpecsDto request,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new UpdateCategorySpecCommand(Id, request.Add, request.Remove));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.CategoryUpdate);

    }
    public record CreateCategoryRequestDto(
        int Order,
        Guid? ParentCategoryId,
        ICollection<Guid> SpecIds,
        LocalizedText Names,
        LocalizedText Descriptions,
        LocalizedText ImageUrls
    );
    public record UpdateCategoryRequestDto(
        int? Order,
        LocalizedText? Names,
        LocalizedText? Descriptions,
        LocalizedText? ImageUrls);
    public record UpdateCategorySpecsDto(ICollection<Guid> Add, ICollection<Guid> Remove);
}
