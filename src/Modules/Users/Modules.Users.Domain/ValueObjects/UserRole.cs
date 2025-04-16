using System.Data;

namespace Modules.Users.Domain.ValueObjects;

public static class UserRole
{
    public static string Memeber => "Member";
    public static string Teacher => "Teacher";
    public static string Admin => "Admin";
    private static string[] Roles = [Memeber, Teacher];
    public static bool IsValidRole(string Role)
    {
        return Roles.Contains(Role);
    }
}
