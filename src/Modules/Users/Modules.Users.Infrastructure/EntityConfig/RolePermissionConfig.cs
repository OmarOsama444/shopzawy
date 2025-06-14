using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Infrastructure.EntityConfig;

public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(
            // Admin permissions (full access)
            new RolePermission { RoleId = "Admin", PermissionId = "banner:create" },
            new RolePermission { RoleId = "Admin", PermissionId = "banner:read" },
            new RolePermission { RoleId = "Admin", PermissionId = "banner:delete" },
            new RolePermission { RoleId = "Admin", PermissionId = "brand:create" },
            new RolePermission { RoleId = "Admin", PermissionId = "brand:read" },
            new RolePermission { RoleId = "Admin", PermissionId = "brand:update" },
            new RolePermission { RoleId = "Admin", PermissionId = "category:create" },
            new RolePermission { RoleId = "Admin", PermissionId = "category:update" },
            new RolePermission { RoleId = "Admin", PermissionId = "category:read" },
            new RolePermission { RoleId = "Admin", PermissionId = "category:delete" },
            new RolePermission { RoleId = "Admin", PermissionId = "color:create" },
            new RolePermission { RoleId = "Admin", PermissionId = "color:read" },
            new RolePermission { RoleId = "Admin", PermissionId = "product:create" },
            new RolePermission { RoleId = "Admin", PermissionId = "product:item:create" },
            new RolePermission { RoleId = "Admin", PermissionId = "product:item:read" },
            new RolePermission { RoleId = "Admin", PermissionId = "product:item:delete" },
            new RolePermission { RoleId = "Admin", PermissionId = "spec:create" },
            new RolePermission { RoleId = "Admin", PermissionId = "spec:read" },
            new RolePermission { RoleId = "Admin", PermissionId = "spec:update" },
            new RolePermission { RoleId = "Admin", PermissionId = "vendor:create" },
            new RolePermission { RoleId = "Admin", PermissionId = "vendor:update" },
            new RolePermission { RoleId = "Admin", PermissionId = "vendor:read" },

            // Default user permissions (read-only for key entities)
            new RolePermission { RoleId = "Default", PermissionId = "banner:read" },
            new RolePermission { RoleId = "Default", PermissionId = "brand:read" },
            new RolePermission { RoleId = "Default", PermissionId = "category:read" },
            new RolePermission { RoleId = "Default", PermissionId = "color:read" },
            new RolePermission { RoleId = "Default", PermissionId = "product:item:read" },
            new RolePermission { RoleId = "Default", PermissionId = "spec:read" },
            new RolePermission { RoleId = "Default", PermissionId = "vendor:read" },

            // Guest user permissions (limited public access)
            new RolePermission { RoleId = "Guest", PermissionId = "banner:read" },
            new RolePermission { RoleId = "Guest", PermissionId = "brand:read" },
            new RolePermission { RoleId = "Guest", PermissionId = "category:read" },
            new RolePermission { RoleId = "Guest", PermissionId = "product:item:read" },
            new RolePermission { RoleId = "Guest", PermissionId = "user:create" },
            new RolePermission { RoleId = "Guest", PermissionId = "auth:login" }
        );

    }

}
