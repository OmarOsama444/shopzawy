using Modules.Users.Domain;
using Modules.Users.Application.Abstractions;
using Modules.Common.Infrastructure.Authentication;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Modules.Users.Application;
using Modules.Users.Domain.Entities;
using System.Security;
using Modules.Users.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace Modules.Users.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly UsersDbContext context;
    public JwtProvider(
        IOptions<JwtOptions> options,
        UsersDbContext usersDbContext)
    {
        _jwtOptions = options.Value;
        context = usersDbContext;
    }
    public async Task<string> GenerateAccesss(User user)
    {
        var permissions = await context.users
            .Where(u => u.Id == user.Id)
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
            .SelectMany(u => u.UserRoles.SelectMany(
                ur => ur.Role.RolePermissions.Select(rp => rp.Permission)))
            .Distinct()
            .ToListAsync();

        var permissionClaims = permissions
            .Select(x => new Claim(CustomClaims.Permission, x.Name))
            .ToArray();

        var claims = new Claim[]
        {
            new(CustomClaims.Sub, user.Id.ToString()),
        }.Concat(permissionClaims)
        .ToArray();

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTimeInMinutes),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler() { MapInboundClaims = false }.WriteToken(token);
    }

    public string GenerateReferesh()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }

}
