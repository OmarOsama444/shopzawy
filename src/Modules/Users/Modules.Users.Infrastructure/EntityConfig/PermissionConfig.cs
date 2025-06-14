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
            // ===================Users=================== //
            Permission.Seed("user:create", true, "Users"),
            Permission.Seed("user:role:update", true, "Users"),
            // roles endpoint
            Permission.Seed("role:read", true, "Users"),
            Permission.Seed("role:create", true, "Users"),
            Permission.Seed("role:permission:update", true, "Users"),
            // permission endpoint
            Permission.Seed("permission:read", true, "Users"),
            Permission.Seed("permission:create", true, "Users"),
            Permission.Seed("permission:update", true, "Users"),
            // auth endpoint
            Permission.Seed("auth:login", true, "Users"),

            // ==================Orders=================== //
            Permission.Seed("banner:create", true, "Orders"),
            Permission.Seed("banner:read", true, "Orders"),
            Permission.Seed("banner:delete", true, "Orders"),
            Permission.Seed("brand:create", true, "Orders"),
            Permission.Seed("brand:read", true, "Orders"),
            Permission.Seed("brand:update", true, "Orders"),
            Permission.Seed("category:create", true, "Orders"),
            Permission.Seed("category:update", true, "Orders"),
            Permission.Seed("category:read", true, "Orders"),
            Permission.Seed("category:delete", true, "Orders"),
            Permission.Seed("color:create", true, "Orders"),
            Permission.Seed("color:read", true, "Orders"),
            Permission.Seed("product:create", true, "Orders"),
            Permission.Seed("product:item:create", true, "Orders"),
            Permission.Seed("product:item:read", true, "Orders"),
            Permission.Seed("product:item:delete", true, "Orders"),
            Permission.Seed("spec:create", true, "Orders"),
            Permission.Seed("spec:read", true, "Orders"),
            Permission.Seed("spec:update", true, "Orders"),
            Permission.Seed("vendor:create", true, "Orders"),
            Permission.Seed("vendor:update", true, "Orders"),
            Permission.Seed("vendor:read", true, "Orders")

        );
    }

}
