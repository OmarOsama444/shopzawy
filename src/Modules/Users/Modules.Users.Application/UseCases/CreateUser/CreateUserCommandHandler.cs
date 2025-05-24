using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Domain;
using Modules.Users.Domain.Exceptions;

namespace Modules.Users.Application.UseCases.CreateUser
{
    public class CreateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
        ) : ICommandHandler<CreateUserCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await userRepository.GetUserByEmail(request.Email) is not null)
                return new UserConflictEmail(request.Email);

            var user = User.Create(
                request.FirstName,
                request.LastName,
                request.HashedPassword,
                request.Role,
                request.Email);
            userRepository.Add(user);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }

}

