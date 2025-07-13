using Common.Application.Extensions;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Catalog.Presentation;
using Modules.Catalog.Application.UseCases.Vendors.CreateVendor;
using Modules.Catalog.Application.UseCases.Vendors.PaginateVendors;
using Modules.Catalog.Application.UseCases.Vendors.UpdateVendor;

namespace Modules.Orders.Presentation.Endpoints;

public class VendorEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/vendors").WithTags("Vendors");

        group.MapPost("", async ([FromBody] CreateVendorCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.VendorCreate);

        group.MapGet("", async ([FromServices] ISender sender, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? namefilter = null) =>
        {
            var result = await sender.Send(new PaginateVendorQuery(pageNumber, pageSize, namefilter));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.VendorRead);

        group.MapPut("{id}", async ([FromRoute] Guid id, [FromBody] VendorUpdateRequestDto request, ISender sender) =>
        {
            var result = await sender.Send(new UpdateVendorCommand(
                id,
                request.VendorName,
                request.Description,
                request.Email,
                request.PhoneNumber,
                request.Address,
                request.LogoUrl,
                request.ShipingZoneName,
                request.Active,
                request.CountryCode));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.VendorUpdate);
    }

    public record VendorUpdateRequestDto(string? VendorName, string? Description, string? Email, string? PhoneNumber, string? Address, string? LogoUrl, string? ShipingZoneName, bool? Active, string CountryCode);
}
