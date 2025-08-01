using System.Security.Claims;
using Common.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication;

namespace Common.Infrastructure.Authorization;

public class ClaimsTransformer : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = (ClaimsIdentity)principal.Identity!;

        var sub = identity.FindFirst(CustomClaims.Sub);
        var nameId = identity.FindFirst(ClaimTypes.NameIdentifier);

        if (sub == null && nameId != null)
        {
            identity.AddClaim(new Claim(CustomClaims.Sub, nameId.Value));
        }

        return Task.FromResult(principal);
    }

}
