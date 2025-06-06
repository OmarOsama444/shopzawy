
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Spec.CreateSpec;

internal class SpecNameValidator : AbstractValidator<KeyValuePair<Language, string>>
{
    public SpecNameValidator()
    {
        RuleFor(x => x.Value).NotEmpty();
        RuleFor(x => x.Key).NotEmpty();
    }
}
