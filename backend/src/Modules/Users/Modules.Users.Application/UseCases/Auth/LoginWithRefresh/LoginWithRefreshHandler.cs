using Common.Application.Messaging;
using Common.Domain;
using Modules.Users.Application.Abstractions;
using Modules.Users.Application.Repositories;
using Modules.Users.Application.Services;
using Modules.Users.Application.UseCases.Auth.LoginWithRefresh;
using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.UseCases.LoginWithRefresh;

public sealed class LoginWithRefreshHandler(
    ITokenRepository tokenRepository,
    IUserRepository userRepository,
    IUserService userService) : ICommandHandler<LoginWithRefreshCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginWithRefreshCommand request, CancellationToken cancellationToken)
    {
        Token? token = await tokenRepository.GetByTokenTypeAndValue(
            request.Token,
            TokenType.Refresh
        );
        if (token is null)
            return new TokenNotFound(request.Token);
        if (token.ExpireDateUtc < DateTime.UtcNow)
            return new TokenExpired(request.Token);
        User? user = await userRepository.GetByIdAsync(token.UserId);
        if (user == null)
            return new UserNotFound(token.UserId);
        return await userService.LoginUser(user, cancellationToken);
    }
}
