using System.Windows.Input;
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.Abstractions;
using Modules.Users.Application.UseCases.LoginUser;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;

namespace Modules.Users.Application.UseCases.LoginWithRefresh;

public record LoginWithRefreshCommand(string Token) : ICommand<LoginUserCommandResponse>;

public sealed class LoginWithRefreshHandler(
    IRefreshRepository refreshReadRepository,
    IRepository<RefreshToken> refreshRepository,
    IUserRepository userRepository,
    IJwtProvider jwtProvider,
    IUnitOfWork unitOfWork) : ICommandHandler<LoginWithRefreshCommand, LoginUserCommandResponse>
{
    public async Task<Result<LoginUserCommandResponse>> Handle(LoginWithRefreshCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await refreshReadRepository.GetByToken(request.Token);
        if (refreshToken == null)
            return new TokenNotFound(request.Token);
        if (refreshToken.ExpiresOnUtc < DateTime.UtcNow)
            return new TokenExpired(request.Token);
        User? user = await userRepository.GetByIdAsync(refreshToken.UserId);
        if (user == null)
            return new UserNotFound(refreshToken.UserId);
        string AccessToken = jwtProvider.GenerateAccesss(user);
        string RefreshToken = jwtProvider.GenerateReferesh();
        refreshRepository.Add(new RefreshToken
        {
            Id = Guid.NewGuid(),
            ExpiresOnUtc = DateTime.UtcNow.AddDays(7),
            Token = RefreshToken,
            UserId = refreshToken.UserId
        });
        await unitOfWork.SaveChangesAsync();
        return new LoginUserCommandResponse(RefreshToken, AccessToken);
    }
}

internal sealed class LoginWithRefreshCommandValidator : AbstractValidator<LoginWithRefreshCommand>
{
    public LoginWithRefreshCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}