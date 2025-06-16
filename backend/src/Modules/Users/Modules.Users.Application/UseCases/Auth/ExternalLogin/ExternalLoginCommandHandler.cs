using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.Repositories;
using Modules.Users.Application.Services;
using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.UseCases.Auth.ExternalLogin;

public class ExternalLoginCommandHandler(
    IUserRepository userRepository,
    IUserService userService,
    IUnitOfWork unitOfWork) : ICommandHandler<ExternalLoginCommand, LoginResponse>
{
    public async Task<Result<LoginResponse>> Handle(ExternalLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByConfirmedEmail(request.Email);
        if (user is not null)
            return await userService.LoginUser(user, cancellationToken);
        user = User.Create(request.GuestId, request.Email, request.FirstName, request.LastName);
        userRepository.Add(user);
        await unitOfWork.SaveChangesAsync();
        return await userService.LoginUser(user, cancellationToken);
    }
}
