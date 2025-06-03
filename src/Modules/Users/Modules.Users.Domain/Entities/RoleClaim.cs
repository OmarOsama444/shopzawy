using Microsoft.AspNetCore.Identity;

namespace Modules.Users.Domain.Entities;

public class RoleClaim : IdentityRoleClaim<Guid>
{
    public Role Role { get; set; } = default!;
}
