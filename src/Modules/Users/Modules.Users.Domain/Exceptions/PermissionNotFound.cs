using Modules.Common.Domain.Exceptions;

namespace Modules.Users.Domain.Exceptions
{
    public class PermissionNotFound : NotFoundException
    {
        public PermissionNotFound(Guid id) : base("Permission.NotFound", $"a permission with this id {id} doesn't exist")
        {
        }

    }
}

