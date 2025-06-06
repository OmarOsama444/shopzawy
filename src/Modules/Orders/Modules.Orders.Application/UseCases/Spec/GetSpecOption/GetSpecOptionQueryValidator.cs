using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.Spec.GetSpecOption;

public class GetSpecOptionQueryValidator : AbstractValidator<GetSpecOptionQuery>
{
    public GetSpecOptionQueryValidator()
    {
        RuleFor(x => x.SpecId).NotEmpty();
    }
}
