using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Users.Application.Abstractions;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.UseCases.LoginUser;

public record LoginUserCommand(string email, string phoneNumber, string password) : IQuery<LoginUserCommandResponse>;
public class LoginUserCommandHandler(
    UserManager<User> userManager,
    IJwtProvider jwtProvider,
    IUserTokenRepository userTokenRepository,
    IUnitOfWork unitOfWork,
    SignInManager<User> signInManager) : IQueryHandler<LoginUserCommand, LoginUserCommandResponse>
{
    public async Task<Result<LoginUserCommandResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager
            .Users
            .FirstOrDefaultAsync(x =>
            (x.Email == request.email && x.EmailConfirmed == true) ||
            (x.PhoneNumber == request.phoneNumber && x.PhoneNumberConfirmed == true));
        if (user is null)
            return new NotAuthorizedException("User.NotAuthorized", "false credintials");
        var result = await signInManager.CheckPasswordSignInAsync(user, request.password, false);

        if (!result.Succeeded)
            return new NotAuthorizedException("User.NotAuthorized", "false credintials");

        string accessToken = jwtProvider.GenerateAccesss(user);
        string refreshToken = jwtProvider.GenerateReferesh();
        var Token = UserToken.Create(TokenType.Refresh, 24 * 60, user.Id, refreshToken);

        userTokenRepository.Add(Token);
        await unitOfWork.SaveChangesAsync();
        return new LoginUserCommandResponse(accessToken: accessToken, refreshToken: refreshToken);
    }
}
public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.email).EmailAddress().When(x => !string.IsNullOrEmpty(x.email));
        RuleFor(x => x.phoneNumber)
            .Must(new PhoneNumberValidator("EG", "Egypt").Must)
            .WithMessage(new PhoneNumberValidator("EG", "Egypt").Message)
            .When(x => !string.IsNullOrEmpty(x.phoneNumber));
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.email) || !string.IsNullOrWhiteSpace(x.phoneNumber))
            .WithMessage("Either email or phone number must be provided.");
        RuleFor(x => x.password)
        .MinimumLength(12).WithMessage("Password must be at least 8 characters long.")
        .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
        .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
        .Matches(@"\d").WithMessage("Password must contain at least one number.")
        .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.")
        .When(x => !String.IsNullOrEmpty(x.password));
    }
}
public record LoginUserCommandResponse(string refreshToken, string accessToken);
