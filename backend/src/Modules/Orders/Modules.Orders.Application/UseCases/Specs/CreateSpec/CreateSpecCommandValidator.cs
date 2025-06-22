
using Common.Application.Validators;
using FluentValidation;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Specs.CreateSpec;

internal class CreateSpecCommandValidator : AbstractValidator<CreateSpecCommand>
{
    public CreateSpecCommandValidator()
    {
        RuleFor(c => c.DataType).NotEmpty();
        RuleForEach(c => c.SpecNames)
            .NotEmpty()
            .SetValidator(new SpecNameValidator());
        RuleFor(c => c.SpecNames).NotEmpty()
            .Must(LanguageValidator.Must)
            .WithMessage(LanguageValidator.Message);
    }
}
