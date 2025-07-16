using Common.Application.Extensions;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Catalog.Application.UseCases.Categories.CreateCategory;
using Modules.Catalog.Application.UseCases.Categories.GetCategory;
using Modules.Catalog.Application.UseCases.Categories.PaginateCategories;
using Modules.Catalog.Application.UseCases.Categories.UpdateCategory;
using Common.Domain.ValueObjects;
using Modules.Catalog.Presentation;

namespace Modules.Catalog.Presentation.Endpoints;

public class CategoryEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/categories").WithTags("Categories");

        group.MapPost("", async ([FromBody] CreateCategoryRequestDto request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateCategoryCommand(
                request.Order,
                request.ParentCategoryId,
                request.SpecIds,
                request.Names.translations,
                request.Descriptions.translations,
                request.ImageUrls.translations
            ));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.CategoryCreate);

        group.MapGet("", async (
            [FromServices] ISender sender,
            [FromQuery] int PageNumber = 1,
            [FromQuery] int PageSize = 50,
            [FromQuery] Language LangCode = Language.en,
            [FromQuery] string? NameFilter = null) =>
        {
            var result = await sender.Send(new PaginateCategoryQuery(
                PageNumber,
                PageSize,
                NameFilter,
                LangCode));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.CategoryRead);

        group.MapGet("{Id}", async (
            [FromRoute] int Id,
            [FromServices] ISender sender,
            [FromQuery] Language LangCode = Language.en) =>
        {
            var result = await sender.Send(new GetCategoryByIdQuery(Id, LangCode));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.CategoryRead);

        group.MapPut("{Id}", async (
            [FromServices] ISender sender,
            [FromRoute] int Id,
            [FromBody] UpdateCategoryRequestDto request) =>
        {
            var result = await sender.Send(new UpdateCategoryCommand(
                Id,
                request.Order,
                request.Specs.Add,
                request.Specs.Remove,
                request.Translations.Add.Names.translations,
                    request.Translations.Add.Descriptions.translations,
                    request.Translations.Add.ImageUrls.translations,
                    request.Translations.Update.Names.translations,
                    request.Translations.Update.Descriptions.translations,
                    request.Translations.Update.ImageUrls.translations,
                    request.Translations.Remove));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.CategoryUpdate);

    }
    public record CreateCategoryRequestDto(
        int Order,
        int? ParentCategoryId,
        ICollection<Guid> SpecIds,
        LocalizedText Names,
        LocalizedText Descriptions,
        LocalizedText ImageUrls
    );
    public record UpdateCategoryRequestDto(
        int? Order,
        UpdateCategorySpecsDto Specs,
        UpdateCategoryRequestTranslations Translations
        );
    public record UpdateCategoryRequestTranslations(CreateCategoryTranslationDto Add, CreateCategoryTranslationDto Update, ICollection<Language> Remove);
    public record CreateCategoryTranslationDto(LocalizedText Names, LocalizedText Descriptions, LocalizedText ImageUrls);
    public record UpdateCategorySpecsDto(ICollection<Guid> Add, ICollection<Guid> Remove);
}
