using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Application.Extensions;
using Modules.Common.Presentation.Endpoints;
using Modules.Users.Application.UseCases.CreateUnverifiedUser;
using Modules.Users.Application.UseCases.CreateUser;

namespace Modules.Users.Presentation.Endpoints.Auth
{
    public class RegisterUserEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/auth/register", async (CreateUnverifiedUserCommand request, ISender sender) =>
            {
                var userId = await sender.Send(request);
                if (userId.isSuccess)
                    return Results.Ok(userId.Value);
                return userId.ExceptionToResult();
            })
            .AllowAnonymous();
        }
    }
}

