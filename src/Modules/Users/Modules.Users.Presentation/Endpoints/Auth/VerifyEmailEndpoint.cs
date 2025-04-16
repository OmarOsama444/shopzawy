using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Application.Extensions;
using Modules.Common.Presentation.Endpoints;
using Modules.Users.Application.UseCases.VerifyEmail;

namespace Modules.Users.Presentation.Endpoints.Auth;

public class VerifyEmailEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/emial/{token}", async (string token, ISender sender) =>
        {
            var result = await sender.Send(new VerifyEmailCommand(token));
            return result.isSuccess ? Results.Redirect("/home") : result.ExceptionToResult();
        });
    }

}
