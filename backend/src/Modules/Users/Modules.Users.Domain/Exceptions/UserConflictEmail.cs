using Modules.Common.Domain.Exceptions;

namespace Modules.Users.Domain.Exceptions
{
    public class UserConflictEmail : ConflictException
    {
        public UserConflictEmail(string Email) : base("User.Conflict.Email", $"User with this email {Email} already exists")
        {
        }

    }
}

