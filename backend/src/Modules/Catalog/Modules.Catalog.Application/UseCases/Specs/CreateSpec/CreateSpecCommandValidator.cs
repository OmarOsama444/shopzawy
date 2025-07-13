
using Common.Application.Validators;
using FluentValidation;

namespace Modules.Catalog.Application.UseCases.Specs.CreateSpec;

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
