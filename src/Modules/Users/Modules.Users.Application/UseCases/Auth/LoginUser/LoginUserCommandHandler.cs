using Microsoft.AspNetCore.Identity;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Users.Application.Abstractions;
using Modules.Users.Application.Repositories;
using Modules.Users.Application.Services;
using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.UseCases.Auth.LoginUser;

public class LoginUserCommandHandler(
    IUserRepository userRepository,
    IUserService userService
    ) : IQueryHandler<LoginUserCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByConfirmedEmail(request.Email);
        if (user is null)
            user = await userRepository.GetByConfirmedPhone(request.PhoneNumber);
        if (user is null)
            return new UserNotFound(Guid.Empty);
        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed)
            return new NotAuthorizedException("User.NotAuthorized", "False credintials");

        return await userService.LoginUser(user);
    }
}
