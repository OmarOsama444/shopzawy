using System.Runtime.InteropServices;
using Modules.Common.Domain.Entities;

namespace Modules.Users.Domain.Entities;

public class Permission : Entity
{
    public string Value { get; set; } = string.Empty;
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = [];

}
