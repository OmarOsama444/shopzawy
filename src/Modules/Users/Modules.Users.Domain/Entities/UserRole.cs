using Microsoft.AspNetCore.Identity;
using Modules.Common.Domain.Entities;

namespace Modules.Users.Domain.Entities;

public class UserRole : Entity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; } = default!;
    public virtual User User { get; set; } = default!;
}
