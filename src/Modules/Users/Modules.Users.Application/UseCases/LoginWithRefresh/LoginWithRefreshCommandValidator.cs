using FluentValidation;

namespace Modules.Users.Application.UseCases.LoginWithRefresh;

internal sealed class LoginWithRefreshCommandValidator : AbstractValidator<LoginWithRefreshCommand>
{
    public LoginWithRefreshCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}