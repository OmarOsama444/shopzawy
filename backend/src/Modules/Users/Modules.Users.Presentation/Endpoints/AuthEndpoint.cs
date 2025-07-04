using System.Security.Claims;
using Common.Application.Extensions;
using Common.Infrastructure.Authentication;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Users.Application.Abstractions;
using Modules.Users.Application.UseCases.Auth.ExternalLogin;
using Modules.Users.Application.UseCases.Auth.LoginGuestUser;
using Modules.Users.Application.UseCases.Auth.LoginUser;
using Modules.Users.Application.UseCases.Auth.LoginWithRefresh;
using Modules.Users.Application.UseCases.VerifyEmail;

namespace Modules.Users.Presentation.Endpoints;

public class AuthEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth").WithTags("Auth");

        group.MapGet("/google", (HttpContext httpContext) =>
        {
            var redirectUrl = $"/external-callback/{httpContext.User.GetUserId()}";
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Results.Challenge(properties, new[] { "Google" });
        })
        .RequireAuthorization(Permissions.LoginUser);

        group.MapGet("/facebook", (HttpContext httpContext) =>
        {
            var redirectUrl = $"/external-callback/{httpContext.User.GetUserId()}";
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Results.Challenge(properties, new[] { "Facebook" });
        })
        .RequireAuthorization(Permissions.LoginUser);

        group.MapGet("/external-callback/{GuestId}",
            async (
                [FromRoute] Guid GuestId,
                HttpContext httpContext,
                [FromServices] IJwtProvider jwtProvider,
                [FromServices] ISender sender) =>
        {
            var email = httpContext.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            var firstName = httpContext.User.FindFirst(ClaimTypes.GivenName)?.Value ?? "";
            var lastName = httpContext.User.FindFirst(ClaimTypes.Surname)?.Value;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(firstName))
                return Results.BadRequest("External login information is incomplete.");

            var result = await sender.Send(new ExternalLoginCommand(GuestId, email, firstName, lastName));

            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        })
        .RequireAuthorization(new AuthorizeAttribute { AuthenticationSchemes = "External" });

        group.MapPost("/refresh", async (LoginWithRefreshCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        })
        .AllowAnonymous();

        group.MapPost("/login", async ([FromBody] LoginUserDto request, [FromServices] ISender sender, HttpContext httpContext) =>
        {
            var result = await sender.Send(new LoginUserCommand(
                request.Email,
                request.PhoneNumber,
                request.CountryCode,
                request.Password,
                httpContext.User.GetUserId()));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        })
        .RequireAuthorization(Permissions.LoginUser);

        group.MapGet("/email/{token}", async ([FromRoute] string token, ISender sender) =>
        {
            token = Uri.UnescapeDataString(token); ;
            var result = await sender.Send(new VerifyEmailCommand(token));
            return result.isSuccess ? Results.Redirect("/home") : result.ExceptionToResult();
        })
        .AllowAnonymous();

        group.MapGet("/guest", async ([FromServices] ISender sender) =>
        {
            var result = await sender.Send(new LoginGuestUserCommand());
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        })
        .AllowAnonymous();
    }

    public record LoginUserDto(string Email, string PhoneNumber, string CountryCode, string Password);
}
