using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.EntityConfig;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder
            .Property(x => x.Name)
            .HasMaxLength(100);
        builder.HasKey(x => x.Name);

        builder
            .HasMany(x => x.UserRoles)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleName);

        builder
            .HasMany(x => x.RolePermissions)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleName);
    }

}
