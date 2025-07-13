using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Users.Application.UseCases.Users.CreateUser;
using Modules.Users.Application.UseCases.Users.UpdateUserRoles;
using Common.Application.Extensions;
using Common.Presentation.Endpoints;

namespace Modules.Users.Presentation.Endpoints;

public class UsersEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/users").WithTags("Users");
        group.MapPost("", async (
            [FromBody] CreateUserDto request,
            [FromServices] ISender sender,
            HttpContext context) =>
        {
            var result = await sender.Send(
                new CreateUserCommand(
                    request.FirstName,
                    request.LastName,
                    request.Password,
                    request.Email
                    )
                );
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        })
        .AllowAnonymous();

        group.MapPut("/{userId}/roles",
            async (
                [FromRoute] Guid userId,
                [FromBody] UpdateUserRolesDto request,
                [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new UpdateUserRolesCommand(userId, request.Add, request.Remove));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        })
        .RequireAuthorization(Permissions.UpdateUserRole);
    }

    public class UpdateUserRolesDto
    {
        public ICollection<string> Add { get; set; } = [];
        public ICollection<string> Remove { get; set; } = [];
    }

    public record CreateUserDto(
        string FirstName,
        string LastName,
        string Password,
        string Email,
        string? PhoneNumber,
        string? CountryCode
    );
}
