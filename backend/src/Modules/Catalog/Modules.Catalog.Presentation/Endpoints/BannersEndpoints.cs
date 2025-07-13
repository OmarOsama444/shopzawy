using Common.Application.Extensions;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Catalog.Presentation;
using Modules.Catalog.Application.UseCases.Banners.CreateBanner;
using Modules.Catalog.Application.UseCases.Banners.DeleteBanner;
using Modules.Catalog.Application.UseCases.Banners.GetActiveBanners;
using Modules.Catalog.Application.UseCases.Banners.PaginateBannersQuery;

namespace Modules.Catalog.Presentation.Endpoints;

public class BannersEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/banners").WithTags("Banners");
        group.MapPost("", async ([FromBody] CreateBannerCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        })
        .RequireAuthorization(Permissions.BannerCreate);

        group.MapGet("active", async ([FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetActiveBannerQuery());
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.BannerRead);

        group.MapGet("", async ([FromServices] ISender sender,
        [FromQuery] string? Title,
        [FromQuery] bool? Active,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50) =>
        {
            var result = await sender.Send(new PaginateBannersQuery(pageNumber, pageSize, Title, Active));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.BannerRead);

        group.MapDelete("{id}", async ([FromRoute] Guid id, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new DeleteBannerCommand(id));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.BannerDelete);
    }

}
