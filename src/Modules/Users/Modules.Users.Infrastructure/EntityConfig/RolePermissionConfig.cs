using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.EntityConfig;

public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder
            .Property(x => x.RoleName)
            .HasMaxLength(100);

        builder.HasKey(x => new { x.RoleName, x.PermissionName });
    }

}
