using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Domain;
using Modules.Users.Domain.Exceptions;

namespace Modules.Users.Application.UseCases.GetUserById;

public sealed class GetUserByIdQueryHandler(IUserRepository userRepository) : IQueryHandler<GetUserByIdQuery, User>
{
    public async Task<Result<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(request.id);
        if (user is null)
            return new UserNotFound(request.id);
        return user;
    }
}