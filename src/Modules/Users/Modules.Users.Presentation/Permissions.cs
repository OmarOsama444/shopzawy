using StackExchange.Redis;

namespace Modules.Users.Presentation;

public static class Permissions
{
    // users endpoint
    public static string CreateUser = "user:create";
    public static string UpdateUserRole = "user:role:update";
    // roles endpoint
    public static string ReadRoles = "role:read";
    public static string CreateRole = "role:create";
    public static string UpdateRolePermissions = "role:permission:update";
    // permission endpoint
    public static string ReadPermissions = "permission:read";
    public static string CreatePermission = "permission:create";
    public static string UpdatePermission = "permission:update";
    // auth endpoint
    public static string LoginUser = "auth:login";
}
