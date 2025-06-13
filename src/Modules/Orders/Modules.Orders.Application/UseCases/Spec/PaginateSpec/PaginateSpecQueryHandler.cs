using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Application.Repositories;

namespace Modules.Orders.Application.UseCases.Spec.PaginateSpec;

public class PaginateSpecQueryHandler(ISpecRepository specRepository) : IQueryHandler<PaginateSpecQuery, PaginationResponse<SpecResponse>>
{
    public async Task<Result<PaginationResponse<SpecResponse>>> Handle(PaginateSpecQuery request, CancellationToken cancellationToken)
    {
        var specs = await specRepository.Paginate(request.PageNumber, request.PageSize, request.Name, request.LangCode);
        int total = await specRepository.Total(request.Name, request.LangCode);
        return new PaginationResponse<SpecResponse>(specs, total, request.PageSize, request.PageNumber);
    }
}
