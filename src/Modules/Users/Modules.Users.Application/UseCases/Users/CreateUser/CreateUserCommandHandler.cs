using Modules.Common.Domain;
using Microsoft.AspNetCore.Identity;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Common.Application.Messaging;
using Modules.Users.Application.Services;
using Modules.Users.Application.Repositories;

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
            if (request.PhoneNumber is not null &&
                await userRepository.GetByConfirmedPhone(request.PhoneNumber) is not null)
                return new UserConflictPhone(request.PhoneNumber);
            var user = User.Create(
                            request.GuestId,
                            request.FirstName,
                            request.LastName,
                            request.Email,
                            request.PhoneNumber,
                            request.CountryCode);


            user = await userService.RegisterUser(user, request.Password);
            return user.Id;
        }
    }

}

