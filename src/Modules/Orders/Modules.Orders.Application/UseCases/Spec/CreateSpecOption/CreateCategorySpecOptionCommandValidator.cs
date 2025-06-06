using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.Spec.CreateSpecOption;

internal class CreateCategorySpecOptionCommandValidator : AbstractValidator<CreateSpecOptionsCommand>
{
    public CreateCategorySpecOptionCommandValidator()
    {
        RuleFor(x => x.SpecId).NotEmpty();
        RuleFor(x => x.Values).NotEmpty();
    }
}