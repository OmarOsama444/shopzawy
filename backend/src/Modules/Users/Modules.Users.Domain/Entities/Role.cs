using Microsoft.AspNetCore.Identity;
using Modules.Common.Domain.Entities;

namespace Modules.Users.Domain.Entities;

public class Role : Entity
{
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedOnUtc { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = [];
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = [];
    public static Role Create(string name)
    {
        return new Role
        {
            Name = name,
            CreatedOnUtc = DateTime.UtcNow
        };
    }
    public static Role Seed(string name)
    {
        return new Role
        {
            Name = name,
            CreatedOnUtc = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        };
    }
}
