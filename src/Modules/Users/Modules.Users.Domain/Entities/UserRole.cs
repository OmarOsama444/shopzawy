using Microsoft.AspNetCore.Identity;

namespace Modules.Users.Domain.Entities;

public class UserRole : IdentityUserRole<Guid>
{
    public User User { get; set; } = default!;
    public Role Role { get; set; } = default!;
}
