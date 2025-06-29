using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Orders.Application.UseCases.CreateBrand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Modules.Orders.Application.UseCases.PaginateBrands;
using Modules.Orders.Application.UseCases.UpdateBrand;
using Common.Domain.ValueObjects;
using Common.Application.Extensions;
using Common.Presentation.Endpoints;

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

        group.MapGet("", async ([FromServices] ISender sender, [FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 10, [FromQuery] string? NameField = null, [FromQuery] Language LangCode = Language.en) =>
        {
            var result = await sender.Send(new PaginateBrandsQuery(PageNumber, PageSize, NameField, LangCode));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.BrandRead);

        group.MapPut("{id}", async ([FromRoute] Guid id, [FromBody] UpdateBrandRequestDto request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(
            new UpdateBrandCommand(id,
            request.LogoUrl,
            request.Featured,
            request.Active,
            request.Translations.Add.Names.translations,
            request.Translations.Add.Descriptions.translations,
            request.Translations.Update.Names.translations,
            request.Translations.Update.Descriptions.translations,
            request.Translations.Remove));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.BrandUpdate);
    }

    public record UpdateBrandRequestDto(
    string? LogoUrl,
    string? Description,
    bool? Featured,
    bool? Active,
    UpdateBrandRequestTranslations Translations);
    public record UpdateBrandTranslationDto(LocalizedText Names, LocalizedText Descriptions);
    public record UpdateBrandRequestTranslations(UpdateBrandTranslationDto Add, UpdateBrandTranslationDto Update, ICollection<Language> Remove);




}
