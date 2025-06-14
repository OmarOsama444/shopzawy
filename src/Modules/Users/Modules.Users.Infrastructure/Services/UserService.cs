using Microsoft.AspNetCore.Identity;
using Modules.Users.Application;
using Modules.Users.Application.Abstractions;
using Modules.Users.Application.Repositories;
using Modules.Users.Application.Services;
using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Infrastructure.Services;

public class UserService(
    IJwtProvider jwtProvider,
    ITokenRepository tokenRepository,
    IUserRepository userRepository,
    IUserRoleRepository userRoleRepository,
    IUnitOfWork unitOfWork) : IUserService
{
    public async Task<LoginResponse> LoginGuest(Guid GuestId, CancellationToken cancellationToken = default)
    {
        string accessToken = await jwtProvider.GenerateGuestAccess(GuestId);
        string refreshToken = jwtProvider.GenerateReferesh();
        var token = Token.Create(TokenType.GuestRefresh, 24 * 60, GuestId, refreshToken);
        tokenRepository.Add(token);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new LoginResponse(AccessToken: accessToken, RefreshToken: refreshToken);
    }

    public async Task<LoginResponse> LoginUser(User user, CancellationToken cancellationToken = default)
    {
        string accessToken = await jwtProvider.GenerateAccesss(user);
        string refreshToken = jwtProvider.GenerateReferesh();
        var token = Token.Create(TokenType.Refresh, 24 * 60, user.Id, refreshToken);
        tokenRepository.Add(token);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new LoginResponse(AccessToken: accessToken, RefreshToken: refreshToken);
    }

    public async Task<User> RegisterUser(User user, string password, CancellationToken cancellationToken)
    {
        var hasher = new PasswordHasher<User>();
        var PasswordHash = hasher.HashPassword(user, password);
        user.SetPassword(PasswordHash);
        userRepository.Add(user);
        if (!string.IsNullOrEmpty(user.Email) && user.EmailConfirmed == false)
        {
            var token = Token
                .Create(
                    TokenType.Email,
                    24 * 60,
                    user.Id,
                    jwtProvider.GenerateReferesh()
                );

            tokenRepository.Add(token);
        }
        if (!string.IsNullOrEmpty(user.PhoneNumber) && user.PhoneNumberConfirmed == false)
        {
            var token = Token
                .Create(
                    TokenType.Phone,
                    5,
                    user.Id,
                    jwtProvider.GenerateReferesh()
                );
            tokenRepository.Add(token);
        }
        userRoleRepository.Add(new UserRole { RoleId = "Default", UserId = user.Id });
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return user;
    }
}
