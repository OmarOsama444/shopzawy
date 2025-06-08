using Microsoft.AspNetCore.Identity;
using Modules.Common.Domain.Entities;

namespace Modules.Users.Domain.Entities;

public class Role : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedOnUtc { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = [];
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = [];
    public static Role Create(string name)
    {
        return new Role
        {
            Name = name,
            Id = Guid.NewGuid(),
            CreatedOnUtc = DateTime.UtcNow
        };
    }
}
