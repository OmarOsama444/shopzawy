using Microsoft.AspNetCore.Identity;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Application.Services;
using Modules.Users.Application.Repositories;
using Common.Domain;
using Common.Application.Messaging;

namespace Modules.Users.Application.UseCases.Users.CreateUser
{
    public class CreateUserCommandHandler(
        IUserRepository userRepository,
        IUserService userService
        ) : ICommandHandler<CreateUserCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Email is not null &&
                await userRepository.GetByConfirmedEmail(request.Email) is not null)
                return new UserConflictEmail(request.Email);

            var user = User.Create(
                            request.FirstName,
                            request.LastName,
                            request.Email);

            user = await userService.RegisterUser(user, request.Password, cancellationToken);
            return user.Id;
        }
    }

}

