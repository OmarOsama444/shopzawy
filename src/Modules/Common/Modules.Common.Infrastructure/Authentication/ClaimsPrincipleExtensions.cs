using System.Security.Claims;


namespace Modules.Common.Infrastructure.Authentication
{
    public static class ClaimsPrincipleExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            string? userId = principal.FindFirst(CustomClaims.Sub)?.Value;
            return Guid.TryParse(userId, out Guid paredUserId) ?
                paredUserId :
                throw new Exception("User Identifier not available");
        }
        public static HashSet<string> GetPermissions(this ClaimsPrincipal? principal)
        {
            IEnumerable<Claim> permissionClaims = principal?.FindAll(CustomClaims.Permission) ??
                                                  throw new Exception("Permissions are unavailable");

            return permissionClaims.Select(c => c.Value).ToHashSet();
        }
    }
}
