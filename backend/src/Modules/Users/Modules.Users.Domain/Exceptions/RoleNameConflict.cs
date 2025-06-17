using Common.Domain.Exceptions;

namespace Modules.Users.Domain.Exceptions
{
    public class RoleNameConflict : ConflictException
    {
        public RoleNameConflict(string name) : base("Role.Conflict.Name", $"a permission with this name {name} already exists")
        {
        }

    }
}

