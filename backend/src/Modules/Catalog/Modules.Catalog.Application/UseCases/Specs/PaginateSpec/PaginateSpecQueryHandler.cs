using Common.Application.Messaging;
using Common.Domain;
using Modules.Catalog.Application.Dtos;
using Modules.Catalog.Application.Repositories;

namespace Modules.Catalog.Application.UseCases.Specs.PaginateSpec;

public class PaginateSpecQueryHandler(ISpecRepository specRepository) : IQueryHandler<PaginateSpecQuery, PaginationResponse<TranslatedSpecResponseDto>>
{
    public async Task<Result<PaginationResponse<TranslatedSpecResponseDto>>> Handle(PaginateSpecQuery request, CancellationToken cancellationToken)
    {
        var specs = await specRepository.Paginate(request.PageNumber, request.PageSize, request.Name, request.LangCode);
        int total = await specRepository.Total(request.Name, request.LangCode);
        return new PaginationResponse<TranslatedSpecResponseDto>(specs, total, request.PageSize, request.PageNumber);
    }
}
