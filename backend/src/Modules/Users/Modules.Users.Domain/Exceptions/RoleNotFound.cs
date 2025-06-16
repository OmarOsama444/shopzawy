using Modules.Common.Domain.Exceptions;

namespace Modules.Users.Domain.Exceptions
{
    public class RoleNotFound : NotFoundException
    {
        public RoleNotFound(string name) : base("Role.NotFound", $"a Role with this name {name} Not Found")
        {
        }

    }
}

