using Modules.Common.Domain.Entities;

namespace Modules.Users.Domain.Entities;

public class RolePermission : Entity
{
    public string RoleName { get; set; } = string.Empty;
    public string PermissionName { get; set; } = string.Empty;
    public virtual Role Role { get; set; } = default!;
    public virtual Permission Permission { get; set; } = default!;
}
