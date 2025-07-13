
using Common.Domain.ValueObjects;
using FluentValidation;

namespace Modules.Catalog.Application.UseCases.Specs.CreateSpec;

internal class SpecNameValidator : AbstractValidator<KeyValuePair<Language, string>>
{
    public SpecNameValidator()
    {
        RuleFor(x => x.Value).NotEmpty();
        RuleFor(x => x.Key).NotEmpty();
    }
}
