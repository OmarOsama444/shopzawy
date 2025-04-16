using Microsoft.AspNetCore.Routing;
using Modules.Common.Presentation.Endpoints;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Common.Application.Extensions;
using Modules.Common.Infrastructure.Authentication.Extensions;
using Modules.Users.Application.UseCases.GetUserById;
using Modules.Users.Application.UseCases;

namespace Modules.Users.Presentation.Endpoints.Users;

public class UsersEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/users").WithTags("Users");

        group.MapGet("id/{id}",
            async (
                [FromRoute] Guid id,
                [FromServices] ISender sender) =>
            {
                var result = await sender.Send(new GetUserByIdQuery(id));
                return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
            }).RequireAuthorization("Admin");

        group.MapGet("",
            async (HttpContext context, ISender sender) =>
            {
                var result = await sender.Send(new GetUserByIdQuery(context.User.GetUserId()));
                return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
            })
        .RequireAuthorization();

        group.MapPut("",
            async (
                [FromBody] UpdateUserRequest request,
                [FromServices] ISender sender,
                [FromServices] ClaimsPrincipal user) =>
            {
                var result = await sender.Send(new UpdateUserNameCommand(
                    user.GetUserId(),
                    request.FirstName,
                    request.LastName));

                if (result.isSuccess)
                    return Results.Ok(result.Value);
                return result.ExceptionToResult();
            })
        .RequireAuthorization();
    }

    public class UpdateUserRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
