using Common.Domain.Exceptions;

namespace Modules.Users.Domain.Exceptions
{
    public class PermissionNameConflict : ConflictException
    {
        public PermissionNameConflict(string name) : base("Permission.Conflict.Name", $"a permission with this name {name} already exists")
        {
        }

    }
}

