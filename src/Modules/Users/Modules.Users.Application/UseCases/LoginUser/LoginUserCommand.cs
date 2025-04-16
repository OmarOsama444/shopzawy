using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Users.Application.Abstractions;
using Modules.Users.Domain;
using Modules.Users.Domain.Exceptions;

namespace Modules.Users.Application.UseCases.LoginUser;

public record LoginUserCommand(Guid userId, string password = "", bool checkpassword = false) : IQuery<LoginUserCommandResponse>;
public class LoginUserCommandHandler(
    IUserRepository userRepository,
    IJwtProvider jwtProvider) : IQueryHandler<LoginUserCommand, LoginUserCommandResponse>
{
    public async Task<Result<LoginUserCommandResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.userId);
        if (user == null)
            return new UserNotFound(request.userId);
        if (request.checkpassword)
        {
            var hasher = new PasswordHasher<Object>();
            if (hasher.VerifyHashedPassword(new object(), user.HashedPassword, request.password) != PasswordVerificationResult.Success)
            {
                return new NotAuthorizedException("Login.Password", $"Incorrect password");
            }
        }
        var (accesstoken, refreshToken) = await jwtProvider.LoginUser(user);
        return new LoginUserCommandResponse(accessToken: accesstoken, refreshToken: refreshToken);
    }
}
public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.userId).NotEmpty();
        RuleFor(x => x.password)
        .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
        .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
        .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
        .Matches(@"\d").WithMessage("Password must contain at least one number.")
        .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.")
        .When(x => !String.IsNullOrEmpty(x.password));
    }
}
public record LoginUserCommandResponse(string refreshToken, string accessToken);
