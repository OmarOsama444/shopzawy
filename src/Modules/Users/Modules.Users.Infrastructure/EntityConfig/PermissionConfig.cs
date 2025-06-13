using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.EntityConfig;

public class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(x => x.Name);
        builder
            .Property(x => x.Name)
            .HasMaxLength(100);
        builder.HasMany(x => x.RolePermissions)
            .WithOne(x => x.Permission)
            .HasForeignKey(x => x.PermissionId);

        // seeding
        builder.HasData(
            Permission.Create("user:create", true, "Users"),
            Permission.Create("user:role:update", true, "Users"),
            // roles endpoint
            Permission.Create("role:read", true, "Users"),
            Permission.Create("role:create", true, "Users"),
            Permission.Create("role:permission:update", true, "Users"),
            // permission endpoint
            Permission.Create("permission:read", true, "Users"),
            Permission.Create("permission:create", true, "Users"),
            Permission.Create("permission:update", true, "Users"),
            // auth endpoint
            Permission.Create("auth:login", true, "Users")
        );
    }

}
