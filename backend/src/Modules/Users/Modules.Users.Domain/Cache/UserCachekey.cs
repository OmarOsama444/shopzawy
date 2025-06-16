namespace Modules.Users.Domain;

public static class UserCachekey
{
    public static string UserId(Guid id) => $"User.Id.{id}";
}
