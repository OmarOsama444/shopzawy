using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.Repositories;
using Modules.Users.Application.Services;
using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.UseCases.Auth.LoginGuestUser;

public class LoginGuestUserCommandHandler(
    IUserService userService
) : ICommandHandler<LoginGuestUserCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginGuestUserCommand request, CancellationToken cancellationToken)
    {
        return await userService.LoginGuest(Guid.NewGuid());
    }
}
