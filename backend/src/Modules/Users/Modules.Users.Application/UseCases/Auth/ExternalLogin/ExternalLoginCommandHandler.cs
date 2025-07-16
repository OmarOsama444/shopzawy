using Common.Application.Messaging;
using Common.Domain;
using Modules.Users.Application.Repositories;
using Modules.Users.Application.Services;
using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;

namespace Modules.Users.Application.UseCases.Auth.ExternalLogin;

public class ExternalLoginCommandHandler(
    IUserRepository userRepository,
    ITokenRepository tokenRepository,
    IUserService userService,
    IUnitOfWork unitOfWork) : ICommandHandler<ExternalLoginCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(ExternalLoginCommand request, CancellationToken cancellationToken)
    {
        Token? token = await tokenRepository.GetByTokenTypeAndValue(request.ProviderToken, request.provider);
        User? user = null;
        if (token is null)
        {
            user = request.Email == null ? null : await userRepository.GetByConfirmedEmail(request.Email);
            if (user is null)
            {
                user = User.Create(request.Email, request.FirstName, request.LastName);
                userRepository.Add(user);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            token = Token.Create(request.provider, 0, user.Id, request.ProviderToken);
            tokenRepository.Add(token);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return await userService.LoginUser(user, cancellationToken);
        }
        else
        {
            user = await userRepository.GetByIdAsync(token.UserId);
            if (user is null)
                return new UserNotFound(token.UserId);
            return await userService.LoginUser(user, cancellationToken);
        }

    }

}
