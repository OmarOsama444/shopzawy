using System.Security.Claims;
using Common.Application.Extensions;
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
using Modules.Users.Application.UseCases.Auth.LoginUser;
using Modules.Users.Application.UseCases.Auth.LoginWithRefresh;
using Modules.Users.Application.UseCases.VerifyEmail;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Presentation.Endpoints;

public class AuthEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth").WithTags("Auth");

        group.MapGet("/google", (HttpContext httpContext) =>
        {
            var redirectUrl = $"/external-callback/google";
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Results.Challenge(properties, new[] { "Google" });
        })
        .AllowAnonymous();

        group.MapGet("/facebook", (HttpContext httpContext) =>
        {
            var redirectUrl = $"/external-callback/facebook";
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Results.Challenge(properties, new[] { "Facebook" });
        })
        .AllowAnonymous();

        group.MapGet("/external-callback/google",
                    async (
                        HttpContext httpContext,
                        [FromServices] IJwtProvider jwtProvider,
                        [FromServices] ISender sender) =>
                {
                    var email = httpContext.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value ?? null;
                    var firstName = httpContext.User.FindFirst(ClaimTypes.GivenName)?.Value ?? null;
                    var lastName = httpContext.User.FindFirst(ClaimTypes.Surname)?.Value ?? null;
                    var id = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (id == null)
                    {
                        throw new Exception("Authentication Failed");
                    }

                    var result = await sender.Send(new ExternalLoginCommand(id, email, firstName, lastName, TokenType.GoogleProvider));

                    return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
                })
                .RequireAuthorization(new AuthorizeAttribute { AuthenticationSchemes = "External" });


        group.MapGet("/external-callback/facebook",
            async (

                HttpContext httpContext,
                [FromServices] IJwtProvider jwtProvider,
                [FromServices] ISender sender) =>
        {
            var email = httpContext.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value ?? null;
            var firstName = httpContext.User.FindFirst(ClaimTypes.GivenName)?.Value ?? null;
            var lastName = httpContext.User.FindFirst(ClaimTypes.Surname)?.Value ?? null;
            var id = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                throw new Exception("Authentication Failed");
            }

            var result = await sender.Send(new ExternalLoginCommand(id, email, firstName, lastName, TokenType.FaceBookProvider));

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
                request.Password
            ));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        })
        .AllowAnonymous();

        group.MapGet("/email/{token}", async ([FromRoute] string token, ISender sender) =>
        {
            token = Uri.UnescapeDataString(token);
            var result = await sender.Send(new VerifyEmailCommand(token));
            return result.isSuccess ? Results.Redirect("/home") : result.ExceptionToResult();
        })
        .AllowAnonymous();
    }

    public record LoginUserDto(string Email, string Password);
}
