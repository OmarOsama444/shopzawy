using System.Security.Cryptography.X509Certificates;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Application.Extensions;
using Modules.Common.Presentation.Endpoints;
using Modules.Orders.Application.UseCases.CreateVendor;
using Modules.Orders.Application.UseCases.PaginateVendors;
using Modules.Orders.Application.UseCases.UpdateVendor;

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
        });

        group.MapGet("", async ([FromServices] ISender sender, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? namefilter = null) =>
        {
            var result = await sender.Send(new PaginateVendorQuery(pageNumber, pageSize, namefilter));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

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
                request.active));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        });
    }

    public record VendorUpdateRequestDto(string? VendorName, string? Description, string? Email, string? PhoneNumber, string? Address, string? LogoUrl, string? ShipingZoneName, bool? active);
}
