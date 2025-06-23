using Modules.Users.Application.Abstractions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Modules.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MassTransit.Util;
using System.Threading.Tasks;
using Modules.Users.Presentation;
using Common.Infrastructure.Authentication;
using Common.Infrastructure;
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
        var permissions = await context.userRoles
            .Where(u => u.UserId == user.Id)
                .Include(ur => ur.Role)
                    .ThenInclude(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
            .SelectMany(u => u.Role.RolePermissions.Select(rp => rp.Permission.Name))
            .Distinct()
            .ToArrayAsync();
        var permissionClaims = permissions.Select(x => new Claim(CustomClaims.Permission, x)).ToArray();

        var claims = new Claim[]
        {
            new(CustomClaims.Sub, user.Id.ToString()),
        }
        .Concat(permissionClaims)
        .ToArray();

        return CreateToken(claims);
    }

    public async Task<string> GenerateGuestAccess(Guid Id)
    {
        var permissions = await
            context.roles
            .Where(x => x.Name == "guest")
            .Include(x => x.RolePermissions)
            .ThenInclude(x => x.Permission)
            .SelectMany(
                x =>
                    x.RolePermissions.Select(
                        rp => rp.Permission.Name
                    )
                )
            .Distinct()
            .ToListAsync();
        var permissionClaims = permissions.Select(x => new Claim(CustomClaims.Permission, x)).ToArray();
        var claims = new Claim[]
        {
            new(CustomClaims.Sub, Id.ToString()),
        }.Concat(permissionClaims)
        .ToArray();
        return CreateToken(claims);
    }

    public string GenerateReferesh()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }

    private string CreateToken(Claim[] claims)
    {
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

}
