using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Infrastructure.Authentication.Extensions
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

        public static string GetRole(this ClaimsPrincipal principal)
        {
            string? role = principal.FindFirst(CustomClaims.Role)?.Value;
            return role ?? throw new Exception("User Role not available");
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            string? Email = principal.FindFirst(CustomClaims.Email)?.Value;
            return Email ?? throw new Exception("User Email not available");
        }
    }
}
