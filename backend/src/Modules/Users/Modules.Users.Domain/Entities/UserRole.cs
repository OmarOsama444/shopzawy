using Common.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Modules.Users.Domain.Entities;

public class UserRole : Entity
{
    public Guid UserId { get; set; }
    public string RoleId { get; set; } = string.Empty;
    public virtual Role Role { get; set; } = default!;
    public virtual User User { get; set; } = default!;
}
