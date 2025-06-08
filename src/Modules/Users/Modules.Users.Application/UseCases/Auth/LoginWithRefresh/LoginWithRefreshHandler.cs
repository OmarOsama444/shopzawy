using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.Abstractions;
using Modules.Users.Application.UseCases.Auth.LoginWithRefresh;
using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.UseCases.LoginWithRefresh;

public sealed class LoginWithRefreshHandler(
    ITokenRepository tokenRepository,
    IUserRepository userRepository,
    IJwtProvider jwtProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<LoginWithRefreshCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(LoginWithRefreshCommand request, CancellationToken cancellationToken)
    {
        Token? token = await tokenRepository.GetByTokenTypeAndValue(
            TokenType.Refresh,
            request.Token
        );

        if (token is null)
            return new TokenNotFound(request.Token);
        if (token.ExpireDateUtc < DateTime.UtcNow)
            return new TokenExpired(request.Token);
        User? user = await userRepository.GetByIdAsync(token.UserId);
        if (user == null)
            return new UserNotFound(token.UserId);
        string accessToken = await jwtProvider.GenerateAccesss(user);
        string refreshToken = jwtProvider.GenerateReferesh();
        var newToken = Token.Create(TokenType.Refresh, 24 * 60, user.Id, refreshToken);

        tokenRepository.Add(newToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new LoginResponse(AccessToken: accessToken, RefreshToken: refreshToken);
    }
}
