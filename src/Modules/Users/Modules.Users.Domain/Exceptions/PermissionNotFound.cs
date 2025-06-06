using Modules.Common.Domain.Exceptions;

namespace Modules.Users.Domain.Exceptions
{
    public class PermissionNotFound : NotFoundException
    {
        public PermissionNotFound(string name) : base("Permission.NotFound", $"a permission with this name {name} doesn't exist")
        {
        }

    }
}

