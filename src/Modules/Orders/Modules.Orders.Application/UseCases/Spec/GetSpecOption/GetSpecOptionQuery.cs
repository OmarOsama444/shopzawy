using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.GetCategorySpecOption;

public record GetSpecOptionQuery(Guid specId) : IQuery<ICollection<SpecOptionResponse>>;

public sealed class GetSpecOptionQueryHandler(
    ISpecOptionRepository specOptionRepository,
    ISpecRepository specRepository
) : IQueryHandler<GetSpecOptionQuery, ICollection<SpecOptionResponse>>
{
    public async Task<Result<ICollection<SpecOptionResponse>>> Handle(GetSpecOptionQuery request, CancellationToken cancellationToken)
    {
        var spec = await specRepository.GetByIdAsync(request.specId);
        if (spec is null)
            return new NotFoundException("Spec.NotFound", $"spec with id {request.specId} not found");
        var specsOptions = await specOptionRepository.GetBySpecId(request.specId);
        return specsOptions.Select(x => new SpecOptionResponse(x.Id, x.SpecificationId, x.Value)).ToList();
    }
}

public class GetSpecOptionQueryValidator : AbstractValidator<GetSpecOptionQuery>
{
    public GetSpecOptionQueryValidator()
    {
        RuleFor(x => x.specId).NotEmpty();
    }
}

public record SpecOptionResponse(Guid Id, Guid SpecificationId, string Value);
