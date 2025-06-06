using System.Web;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Application.Extensions;
using Modules.Common.Presentation.Endpoints;
using Modules.Users.Application.Abstractions;
using Modules.Users.Application.UseCases.LoginUser;
using Modules.Users.Application.UseCases.LoginWithRefresh;
using Modules.Users.Application.UseCases.VerifyEmail;

namespace Modules.Users.Presentation.Endpoints;

public class AuthEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth").WithTags("Auth");

        group.MapGet("/google", () =>
        {
            var redirectUrl = "external-callback";
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Results.Challenge(properties, new[] { "Facebook" });
        });

        group.MapGet("/facebook", () =>
        {
            var redirectUrl = "external-callback";
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Results.Challenge(properties, new[] { "Facebook" });
        });

        group.MapGet("/external-callback",
            (
                HttpContext httpContext,
                IJwtProvider jwtProvider,
                ISender sender) =>
        {
            throw new NotImplementedException("not implemented yet interface");
        });

        group.MapPost("/refresh", async (LoginWithRefreshCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        })
        .AllowAnonymous();

        group.MapPost("/login", async (LoginUserCommand request, ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        })
        .AllowAnonymous();

        group.MapGet("/email/{token}", async ([FromRoute] string token, ISender sender) =>
        {
            token = HttpUtility.UrlDecode(token); ;
            var result = await sender.Send(new VerifyEmailCommand(token));
            return result.isSuccess ? Results.Redirect("/home") : result.ExceptionToResult();
        })
        .AllowAnonymous();
    }

}
