using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Application.Extensions;
using Modules.Common.Presentation.Endpoints;
using Modules.Users.Application.UseCases.CreateRole;
using Modules.Users.Application.UseCases.AddPermissionToRole;

namespace Modules.Users.Presentation.Endpoints;

public class RolesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/roles").WithTags("Roles");
        group.MapPost("", async ([FromBody] CreateRoleCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapPost("/{RoleName}",
            async ([FromRoute] string RoleName, [FromBody] AddPermissionToRoleCommand request, [FromServices] ISender sender) =>
            {
                var result = await sender.Send(request);
                return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
            });
    }

}
