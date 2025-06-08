using System.Globalization;
using Modules.Common.Domain.Entities;

namespace Modules.Users.Domain.Entities;

public class Permission : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public bool Active { get; private set; } = true;
    public DateTime CreatedOnUtc { get; set; }
    public string? Module { get; private set; }
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = [];
    public static Permission Create(string name, bool active, string? module = null)
    {
        return new Permission()
        {
            Name = name,
            Active = active,
            Module = module,
            Id = Guid.NewGuid(),
            CreatedOnUtc = DateTime.UtcNow
        };
    }

    public void Update(string? name = null, bool? active = null, string? module = null)
    {
        if (!string.IsNullOrEmpty(name))
            this.Name = name;
        if (active.HasValue)
            this.Active = active.Value;
        if (!string.IsNullOrEmpty(module))
            this.Module = module;
    }
}
