using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Application.Extensions;
using Modules.Common.Presentation.Endpoints;
using Modules.Orders.Application.UseCases.Banners.CreateBanner;
using Modules.Orders.Application.UseCases.Banners.DeleteBanner;
using Modules.Orders.Application.UseCases.Banners.GetActiveBanners;
using Modules.Orders.Application.UseCases.Banners.PaginateBannersQuery;

namespace Modules.Orders.Presentation.Endpoints;

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
