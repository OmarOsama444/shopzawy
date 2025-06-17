using Common.Domain.Entities;

namespace Modules.Users.Domain.Entities;

public class RolePermission : Entity
{
    public string RoleId { get; set; } = string.Empty;
    public string PermissionId { get; set; } = string.Empty;
    public virtual Role Role { get; set; } = default!;
    public virtual Permission Permission { get; set; } = default!;
}
