
using Common.Domain.ValueObjects;
using FluentValidation;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Specs.CreateSpec;

internal class SpecNameValidator : AbstractValidator<KeyValuePair<Language, string>>
{
    public SpecNameValidator()
    {
        RuleFor(x => x.Value).NotEmpty();
        RuleFor(x => x.Key).NotEmpty();
    }
}
