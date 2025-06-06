using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Application.UseCases.Spec.Dtos;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.Spec.GetSpecOption;

public sealed class GetSpecOptionQueryHandler(
    ISpecOptionRepository specOptionRepository,
    ISpecRepository specRepository
) : IQueryHandler<GetSpecOptionQuery, ICollection<SpecOptionResponse>>
{
    public async Task<Result<ICollection<SpecOptionResponse>>> Handle(GetSpecOptionQuery request, CancellationToken cancellationToken)
    {
        var spec = await specRepository.GetByIdAsync(request.SpecId);
        if (spec is null)
            return new NotFoundException("Spec.NotFound", $"spec with id {request.SpecId} not found");
        var specsOptions = await specOptionRepository.GetBySpecId(request.SpecId);
        return specsOptions.Select(x => new SpecOptionResponse(x.Id, x.SpecificationId, x.Value)).ToList();
    }
}
