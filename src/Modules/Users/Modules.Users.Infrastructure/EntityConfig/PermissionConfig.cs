using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.EntityConfig;

public class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(x => x.Value);

        builder.HasMany(x => x.RolePermissions)
            .WithOne(x => x.Permission)
            .HasForeignKey(x => x.PermissionName);
    }

}
