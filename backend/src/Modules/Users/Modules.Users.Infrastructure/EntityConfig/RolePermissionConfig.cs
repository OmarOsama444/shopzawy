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
            // admin permissions (full access)
            new RolePermission { RoleId = "admin", PermissionId = "banner:create" },
            new RolePermission { RoleId = "admin", PermissionId = "banner:read" },
            new RolePermission { RoleId = "admin", PermissionId = "banner:delete" },
            new RolePermission { RoleId = "admin", PermissionId = "brand:create" },
            new RolePermission { RoleId = "admin", PermissionId = "brand:read" },
            new RolePermission { RoleId = "admin", PermissionId = "brand:update" },
            new RolePermission { RoleId = "admin", PermissionId = "category:create" },
            new RolePermission { RoleId = "admin", PermissionId = "category:update" },
            new RolePermission { RoleId = "admin", PermissionId = "category:read" },
            new RolePermission { RoleId = "admin", PermissionId = "category:delete" },
            new RolePermission { RoleId = "admin", PermissionId = "color:create" },
            new RolePermission { RoleId = "admin", PermissionId = "color:read" },
            new RolePermission { RoleId = "admin", PermissionId = "product:create" },
            new RolePermission { RoleId = "admin", PermissionId = "product:item:create" },
            new RolePermission { RoleId = "admin", PermissionId = "product:item:read" },
            new RolePermission { RoleId = "admin", PermissionId = "product:item:delete" },
            new RolePermission { RoleId = "admin", PermissionId = "spec:create" },
            new RolePermission { RoleId = "admin", PermissionId = "spec:read" },
            new RolePermission { RoleId = "admin", PermissionId = "spec:update" },
            new RolePermission { RoleId = "admin", PermissionId = "vendor:create" },
            new RolePermission { RoleId = "admin", PermissionId = "vendor:update" },
            new RolePermission { RoleId = "admin", PermissionId = "vendor:read" },
            // User management permissions
            new RolePermission { RoleId = "admin", PermissionId = "role:read" },
            new RolePermission { RoleId = "admin", PermissionId = "role:create" },
            new RolePermission { RoleId = "admin", PermissionId = "role:permission:update" },
            new RolePermission { RoleId = "admin", PermissionId = "user:create" },
            new RolePermission { RoleId = "admin", PermissionId = "user:role:update" },
            new RolePermission { RoleId = "admin", PermissionId = "permission:read" },
            new RolePermission { RoleId = "admin", PermissionId = "permission:create" },
            new RolePermission { RoleId = "admin", PermissionId = "permission:update" },
            // default user permissions (read-only for key entities)
            new RolePermission { RoleId = "default", PermissionId = "banner:read" },
            new RolePermission { RoleId = "default", PermissionId = "brand:read" },
            new RolePermission { RoleId = "default", PermissionId = "category:read" },
            new RolePermission { RoleId = "default", PermissionId = "color:read" },
            new RolePermission { RoleId = "default", PermissionId = "product:item:read" },
            new RolePermission { RoleId = "default", PermissionId = "spec:read" },
            new RolePermission { RoleId = "default", PermissionId = "vendor:read" },

            // guest user permissions (limited public access)
            new RolePermission { RoleId = "guest", PermissionId = "banner:read" },
            new RolePermission { RoleId = "guest", PermissionId = "brand:read" },
            new RolePermission { RoleId = "guest", PermissionId = "category:read" },
            new RolePermission { RoleId = "guest", PermissionId = "product:item:read" },
            new RolePermission { RoleId = "guest", PermissionId = "user:create" },
            new RolePermission { RoleId = "guest", PermissionId = "auth:login" }
        );

    }

}
