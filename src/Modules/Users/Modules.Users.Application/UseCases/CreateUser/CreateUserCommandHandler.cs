using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.Abstractions;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.UseCases.CreateUser
{
    public class CreateUserCommandHandler(
        IUserTokenRepository userTokenRepository,
        UserManager<User> userManager,
        IUnitOfWork unitOfWork,
        IJwtProvider jwtProvider
        ) : ICommandHandler<CreateUserCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await userManager
                .Users
                .AnyAsync(x => x.Email == request.Email && x.EmailConfirmed == true))
                return new UserConflictEmail(request.Email);
            if (await userManager
                .Users
                .AnyAsync(x => x.PhoneNumber == request.PhoneNumber
                && x.PhoneNumberConfirmed == true))
                return new UserConflictPhone(request.PhoneNumber);

            var user = User.Create(
                request.FirstName,
                request.LastName,
                request.Email,
                request.PhoneNumber);

            var CreatedUser = await userManager.CreateAsync(user, request.Password);

            var emailToken = UserToken
                .Create(
                    TokenType.Email,
                    24 * 60,
                    user.Id,
                    jwtProvider.GenerateReferesh()
                );

            userTokenRepository.Add(emailToken);

            await unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }

}

