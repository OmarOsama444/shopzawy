using FluentValidation;

namespace Modules.Users.Application.UseCases.Auth.LoginGuestUser;

internal class LoginGuestUserCommandValidator : AbstractValidator<LoginGuestUserCommand>
{
    public LoginGuestUserCommandValidator()
    {
    }
}
