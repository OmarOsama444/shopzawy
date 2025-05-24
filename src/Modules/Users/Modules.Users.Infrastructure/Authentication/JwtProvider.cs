using Modules.Users.Domain;
using Modules.Users.Application.Abstractions;
using Modules.Common.Infrastructure.Authentication;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Modules.Users.Domain.Entities;
using Modules.Users.Application;
using System.Threading.Tasks;
namespace Modules.Users.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private IRefreshRepository refreshRepository;
    private IUnitOfWork unitOfWork;
    public JwtProvider(
        IOptions<JwtOptions> options,
        IRefreshRepository refreshRepository,
        IUnitOfWork unitOfWork)
    {
        _jwtOptions = options.Value;
        this.refreshRepository = refreshRepository;
        this.unitOfWork = unitOfWork;
    }
    public string GenerateAccesss(User user)
    {
        var claims = new Claim[]
        {
            new(CustomClaims.Sub, user.Id.ToString()),
            new(CustomClaims.Email, user.Email),
            new(CustomClaims.Role, user.Role),
        };

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

    public async Task<(string accesstoken, string refreshtoken)> LoginUser(User user)
    {
        var refreshToken = new RefreshToken
        {
            ExpiresOnUtc = DateTime.UtcNow.AddDays(7),
            Id = Guid.NewGuid(),
            Token = GenerateReferesh(),
            UserId = user.Id
        };

        refreshRepository.Add(refreshToken);
        await unitOfWork.SaveChangesAsync();

        return new(
            refreshToken.Token,
            GenerateAccesss(user)
        );

    }
}
