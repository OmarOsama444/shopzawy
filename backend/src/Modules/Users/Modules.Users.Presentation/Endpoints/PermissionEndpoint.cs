using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Presentation.Endpoints;
using MediatR;
using Modules.Common.Application.Extensions;
using Modules.Users.Application.UseCases.Permissions.GetAllPermissions;
using Modules.Users.Application.UseCases.Permissions.UpdatePermission;
using Modules.Users.Application.UseCases.Permissions.PaginatePermissions;
using Modules.Users.Application.UseCases.Permissions.CreatePermission;
namespace Modules.Users.Presentation.Endpoints;

public class PermissionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/permission").WithTags("Permissions");
        group.MapGet("all", async ([FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetAllPermissionsQuery());
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.ReadPermissions);

        group.MapGet("", async ([FromQuery] int PageSize, [FromQuery] int PageNumber, [FromQuery] string? Name, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new PaginatePermissionsQuery(PageNumber, PageSize, Name));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.ReadPermissions);

        group.MapPost("", async ([FromBody] CreatePermissionCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.CreatePermission);

        group.MapPut("/{PermissionId}", async ([FromRoute] string PermissionId, [FromBody] UpdatePermissionDto request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new UpdatePermissionCommand(
                PermissionId,
                request.PermissionName,
                request.ModuleName,
                request.Active));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).RequireAuthorization(Permissions.UpdatePermission);
    }
    public record UpdatePermissionDto(string? PermissionName, string? ModuleName, bool? Active);
}
