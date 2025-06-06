using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Presentation.Endpoints;
using Modules.Users.Application.UseCases.CreatePermission;
using MediatR;
using Modules.Common.Application.Extensions;
namespace Modules.Users.Presentation.Endpoints;

public class PermissionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/permission").WithTags("Permissions");

        group.MapPost("", async ([FromBody] CreatePermissionCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });
    }

}
