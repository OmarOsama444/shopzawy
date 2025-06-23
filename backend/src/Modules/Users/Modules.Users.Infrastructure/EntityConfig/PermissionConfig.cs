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
            Permission.Seed("user:create", true, "users"),
            Permission.Seed("user:role:update", true, "users"),
            // roles endpoint
            Permission.Seed("role:read", true, "users"),
            Permission.Seed("role:create", true, "users"),
            Permission.Seed("role:permission:update", true, "users"),
            // permission endpoint
            Permission.Seed("permission:read", true, "users"),
            Permission.Seed("permission:create", true, "users"),
            Permission.Seed("permission:update", true, "users"),
            // auth endpoint
            Permission.Seed("auth:login", true, "users"),

            // ==================Orders=================== //
            Permission.Seed("banner:create", true, "orders"),
            Permission.Seed("banner:read", true, "orders"),
            Permission.Seed("banner:delete", true, "orders"),
            Permission.Seed("brand:create", true, "orders"),
            Permission.Seed("brand:read", true, "orders"),
            Permission.Seed("brand:update", true, "orders"),
            Permission.Seed("category:create", true, "orders"),
            Permission.Seed("category:update", true, "orders"),
            Permission.Seed("category:read", true, "orders"),
            Permission.Seed("category:delete", true, "orders"),
            Permission.Seed("color:create", true, "orders"),
            Permission.Seed("color:read", true, "orders"),
            Permission.Seed("product:create", true, "orders"),
            Permission.Seed("product:item:create", true, "orders"),
            Permission.Seed("product:item:read", true, "orders"),
            Permission.Seed("product:item:delete", true, "orders"),
            Permission.Seed("spec:create", true, "orders"),
            Permission.Seed("spec:read", true, "orders"),
            Permission.Seed("spec:update", true, "orders"),
            Permission.Seed("vendor:create", true, "orders"),
            Permission.Seed("vendor:update", true, "orders"),
            Permission.Seed("vendor:read", true, "orders")

        );
    }

}
