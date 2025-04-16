using Modules.Common.Domain.Exceptions;

namespace Modules.Users.Domain.Exceptions
{
    public class UserNotFound : NotFoundException
    {
        public UserNotFound(Guid id) : base("User.NotFound", $"User with id {id} not found")
        {
        }

    }
}

