using System.Data;
using FluentValidation;

namespace Modules.Users.Application.UseCases.VerifyEmail;

internal sealed class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
{
    public VerifyEmailCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}