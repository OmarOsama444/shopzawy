using System.Windows.Input;
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.Abstractions;
using Modules.Users.Application.UseCases.LoginUser;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.UseCases.LoginWithRefresh;

public record LoginWithRefreshCommand(string Token) : ICommand<LoginUserCommandResponse>;

public sealed class LoginWithRefreshHandler(
    IUserRepository userRepository,
    IUserTokenRepository userTokenRepository,
    IJwtProvider jwtProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<LoginWithRefreshCommand, LoginUserCommandResponse>
{
    public async Task<Result<LoginUserCommandResponse>> Handle(LoginWithRefreshCommand request, CancellationToken cancellationToken)
    {
        UserToken? Token = await userTokenRepository.GetToken(request.Token, TokenType.Refresh);
        if (Token is null)
            return new TokenNotFound(request.Token);
        if (Token.ExpireDateUtc < DateTime.UtcNow)
            return new TokenExpired(request.Token);
        User? user = await userRepository.GetByIdAsync(Token.UserId);
        if (user == null)
            return new UserNotFound(Token.UserId);
        string accessToken = jwtProvider.GenerateAccesss(user);
        string refreshToken = jwtProvider.GenerateReferesh();
        var newToken = UserToken.Create(TokenType.Refresh, 24 * 60, user.Id, refreshToken);

        userTokenRepository.Add(Token);
        await unitOfWork.SaveChangesAsync();
        return new LoginUserCommandResponse(accessToken: accessToken, refreshToken: refreshToken);
    }
}

internal sealed class LoginWithRefreshCommandValidator : AbstractValidator<LoginWithRefreshCommand>
{
    public LoginWithRefreshCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}