using Modules.Common.Domain.Entities;

namespace Modules.Users.Domain.Entities;

public class RolePermission : Entity
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    public virtual Role Role { get; set; } = default!;
    public virtual Permission Permission { get; set; } = default!;
}
