using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Application.Extensions;
using Modules.Common.Presentation.Endpoints;
using Modules.Users.Application.UseCases.Roles.CreateRole;
using Modules.Users.Application.UseCases.Roles.GetRoleById;
using Modules.Users.Application.UseCases.Roles.PaginateRoles;
using Modules.Users.Application.UseCases.Roles.UpdateRolePermissions;

namespace Modules.Users.Presentation.Endpoints;

public class RolesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/roles").WithTags("Roles");

        group.MapGet("", async ([FromQuery] int PageSize, [FromQuery] int PageNumber, [FromQuery] string? Name, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new PaginateRolesQuery(PageNumber, PageSize, Name));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.Map("/{Id}", async ([FromRoute] Guid Id, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetRoleByIdQuery(Id));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapPost("", async ([FromBody] CreateRoleCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapPost("/{RoleId}/permissions",
        async ([FromRoute] Guid RoleId, [FromBody] UpdateRolePermissionsDto request, [FromServices] ISender sender) =>
        {
            var result = await sender
                .Send(new UpdateRolePermissionsCommand(RoleId, request.add, request.remove));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        });

    }
    public class UpdateRolePermissionsDto
    {
        public ICollection<Guid> add { get; set; } = [];
        public ICollection<Guid> remove { get; set; } = [];
    }
}
