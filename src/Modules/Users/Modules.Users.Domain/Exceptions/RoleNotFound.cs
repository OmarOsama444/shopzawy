using Modules.Common.Domain.Exceptions;

namespace Modules.Users.Domain.Exceptions
{
    public class RoleNotFound : NotFoundException
    {
        public RoleNotFound(Guid id) : base("Role.NotFound", $"a Role with this id {id} Not Found")
        {
        }

    }
}

