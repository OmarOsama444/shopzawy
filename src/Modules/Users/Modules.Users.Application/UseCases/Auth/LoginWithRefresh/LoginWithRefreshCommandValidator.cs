using FluentValidation;

namespace Modules.Users.Application.UseCases.Auth.LoginWithRefresh;

internal sealed class LoginWithRefreshCommandValidator : AbstractValidator<LoginWithRefreshCommand>
{
    public LoginWithRefreshCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}