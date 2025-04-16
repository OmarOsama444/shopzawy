using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Application.Extensions;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Common.Presentation.Endpoints;
using Modules.Users.Application.Abstractions;
using Modules.Users.Application.UseCases.CreateUser;
using Modules.Users.Application.UseCases.GetUserByEmail;
using Modules.Users.Application.UseCases.GetUserById;
using Modules.Users.Application.UseCases.LoginUser;
using Modules.Users.Domain;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Presentation.Endpoints.Auth;

public class GoogleAuthEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("auth/google", () =>
        {
            var redirectUrl = "external-callback";
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Results.Challenge(properties, new[] { "Facebook" });
        });

        app.MapGet("auth/facebook", () =>
        {
            var redirectUrl = "external-callback";
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Results.Challenge(properties, new[] { "Facebook" });
        });

        app.MapGet("auth/external-callback",
            async (
                HttpContext httpContext,
                IJwtProvider jwtProvider,
                ISender sender,
                IUserRepository userRepository) =>
        {
            var result = await httpContext.AuthenticateAsync("External");
            if (!result.Succeeded)
                return Results.Unauthorized();

            string email = result.Principal.FindFirstValue(ClaimTypes.Email)!;
            string name = result.Principal.Identity?.Name ?? "Unknown";
            // var userResult = await sender.Send(new GetUserByEmailQuery(email));
            // Guid userId = Guid.Empty;
            // if (userResult.isSuccess)
            // {
            //     userId = userResult.Value.Id;
            // }
            // else if (userResult.exception is NotFoundException)
            // {
            //     var createUserResult = await sender.Send(new CreateUserCommand(name, null, null, UserRole.Memeber, email, null));
            //     userId = createUserResult.Value;
            // }
            // else
            // {

            // }
            // var loginResult = await sender.Send(new LoginUserCommand(userId: userId));
            // return loginResult.isSuccess ? Results.Ok(loginResult.Value) : loginResult.ExceptionToResult();
            throw new NotImplementedException("not implemented yet interface");
        });
    }

}
