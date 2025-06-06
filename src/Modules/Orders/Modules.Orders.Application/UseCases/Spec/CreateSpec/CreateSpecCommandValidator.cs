
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Spec.CreateSpec;

internal class CreateSpecCommandValidator : AbstractValidator<CreateSpecCommand>
{
    public CreateSpecCommandValidator()
    {
        RuleFor(c => c.DataType).NotEmpty().Must(SpecDataType.ValidKey).WithMessage("invalid datatype");
        RuleForEach(c => c.SpecNames)
            .NotEmpty()
            .SetValidator(new SpecNameValidator());
    }
}
