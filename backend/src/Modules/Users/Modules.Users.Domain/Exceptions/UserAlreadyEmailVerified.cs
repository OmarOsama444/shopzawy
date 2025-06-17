using Common.Domain.Exceptions;

namespace Modules.Users.Domain.Exceptions
{
    public class UserAlreadyEmailVerified : ConflictException
    {
        public UserAlreadyEmailVerified(string email) :
            base("User.Email.Conflict", $"User with this email {email} already verified")
        {

        }
    }
}

