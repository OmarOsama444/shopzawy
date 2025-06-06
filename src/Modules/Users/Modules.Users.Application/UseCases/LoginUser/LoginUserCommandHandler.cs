using Microsoft.AspNetCore.Identity;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Users.Application.Abstractions;
using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.UseCases.LoginUser;

public class LoginUserCommandHandler(
    IJwtProvider jwtProvider,
    IUserRepository userRepository,
    ITokenRepository tokenRepository,
    IUnitOfWork unitOfWork) : IQueryHandler<LoginUserCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByConfirmedEmail(request.Email);
        if (user is null)
            user = await userRepository.GetByConfirmedPhone(request.PhoneNumber);

        if (user is null)
            return new NotAuthorizedException("User.NotAuthorized", "false credintials");
        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed)
            return new NotAuthorizedException("User.NotAuthorized", "false credintials");

        string accessToken = jwtProvider.GenerateAccesss(user);
        string refreshToken = jwtProvider.GenerateReferesh();
        var token = Token.Create(TokenType.Refresh, 24 * 60, user.Id, refreshToken);

        tokenRepository.Add(token);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new LoginResponse(accessToken: accessToken, refreshToken: refreshToken);
    }
}
