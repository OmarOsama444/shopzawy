using Microsoft.AspNetCore.Routing;
using Modules.Common.Presentation.Endpoints;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Common.Application.Extensions;
using Modules.Users.Application.UseCases.CreateUser;

namespace Modules.Users.Presentation.Endpoints;

public class UsersEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/users").WithTags("Users");
        group.MapPost("", async ([FromBody] CreateUserCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        }).AllowAnonymous();
    }
}
