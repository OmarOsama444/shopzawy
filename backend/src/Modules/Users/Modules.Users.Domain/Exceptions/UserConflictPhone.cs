using Common.Domain.Exceptions;

namespace Modules.Users.Domain.Exceptions
{
    public class UserConflictPhone : ConflictException
    {
        public UserConflictPhone(string phone) : base("User.Conflict.Phone", "User with this phone {phone} already exists") { }
    }
}

