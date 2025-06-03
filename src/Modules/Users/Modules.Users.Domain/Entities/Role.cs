using Microsoft.AspNetCore.Identity;

namespace Modules.Users.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public ICollection<UserRole> UserRoles { get; set; } = default!;
    public ICollection<RoleClaim> RoleClaims { get; set; } = default!;
    public static Role Create(string name)
    {
        return new Role
        {
            Id = Guid.NewGuid(),
            Name = name,
            NormalizedName = name.ToUpper()
        };
    }
}
