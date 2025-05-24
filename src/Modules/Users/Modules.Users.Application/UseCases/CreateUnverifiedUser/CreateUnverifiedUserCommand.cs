using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Application.UseCases.CreateUnverifiedUser;

public record CreateUnverifiedUserCommand(string FirstName, string LastName, string Password, string Role, string Email, string PhoneNumber) : ICommand<Guid>;

public sealed class CreateUnverifiedUserCommandHandler(
    IUserRepository userRepository,
    IUnverifiedUserRepository unverifiedUserRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateUnverifiedUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUnverifiedUserCommand request, CancellationToken cancellationToken)
    {
        if (!String.IsNullOrEmpty(request.Email) && await userRepository.GetUserByEmail(request.Email) is not null)
            return new UserConflictEmail(request.Email);
        PasswordHasher<object> passwordHasher = new PasswordHasher<object>();
        string HashedPassword = passwordHasher.HashPassword(new object(), request.Password);
        var user = UnverifiedUser.Create(
            request.FirstName,
            request.LastName,
            HashedPassword,
            request.Role,
            request.Email,
            request.PhoneNumber);
        unverifiedUserRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}

internal class CreateUnverifiedUserValidator : AbstractValidator<CreateUnverifiedUserCommand>
{
    public CreateUnverifiedUserValidator()
    {
        RuleFor(x => x.FirstName)
               .NotEmpty().WithMessage("First name is required.")
               .MinimumLength(2).WithMessage("First name must be at least 3 characters long.")
               .MaximumLength(20).WithMessage("First name must not exceed 20 characters.");
        RuleFor(x => x.LastName)
            .MaximumLength(20).WithMessage("Last name must not exceed 20 characters.")
            .MinimumLength(2).WithMessage("Last name must be at least 3 characters long.")
            .When(x => !string.IsNullOrEmpty(x.LastName));
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email));
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^(?:\+20|0)(10|11|12|15)\d{8}$")
            .WithMessage("Invalid Egyptian phone number. It must start with +20 or 0, followed by 10, 11, 12, or 15, and contain 8 more digits.")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        RuleFor(x => x.Role).NotEmpty().Must(role => UserRole.IsValidRole(role)).WithMessage("Invalid role");
        RuleFor(x => x)
            .Must(x => !string.IsNullOrEmpty(x.Email) || !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage("You must provide at least an email or a phone number.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one digit.")
            .Matches(@"[@$!%*?&]").WithMessage("Password must contain at least one special character (@, $, !, %, *, ?, &).")
            .When(x => !string.IsNullOrEmpty(x.Password));
    }
}