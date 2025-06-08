using Modules.Users.Domain;
using Modules.Common.Domain;
using Microsoft.AspNetCore.Identity;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;
using Modules.Users.Domain.ValueObjects;
using Modules.Common.Application.Messaging;
using Modules.Users.Application.Abstractions;

namespace Modules.Users.Application.UseCases.Users.CreateUser
{
    public class CreateUserCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        ITokenRepository tokenRepository,
        IJwtProvider jwtProvider
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
                request.FirstName,
                request.LastName,
                request.Email,
                request.PhoneNumber,
                request.CountryCode);

            var hasher = new PasswordHasher<User>();
            var PasswordHash = hasher.HashPassword(user, request.Password);

            user.SetPassword(PasswordHash);

            userRepository.Add(user);
            if (!string.IsNullOrEmpty(request.Email))
            {
                var token = Token
                    .Create(
                        TokenType.Email,
                        24 * 60,
                        user.Id,
                        jwtProvider.GenerateReferesh()
                    );

                tokenRepository.Add(token);
            }
            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                var token = Token
                    .Create(
                        TokenType.Phone,
                        5,
                        user.Id,
                        jwtProvider.GenerateReferesh()
                    );
                tokenRepository.Add(token);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }

}

