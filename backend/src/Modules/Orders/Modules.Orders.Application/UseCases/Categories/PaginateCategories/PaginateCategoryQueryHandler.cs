using Common.Application.Messaging;
using Common.Domain;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Application.Repositories;

namespace Modules.Orders.Application.UseCases.PaginateCategories;

public sealed class PaginateCategoryQueryHandler(ICategoryRepository categoryRepository) : IQueryHandler<PaginateCategoryQuery, PaginationResponse<CategoryPaginationResponseDto>>
{
    public async Task<Result<PaginationResponse<CategoryPaginationResponseDto>>> Handle(PaginateCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.Paginate(request.PageNumber, request.PageSize, request.NameFilter, request.LangCode);
        var total = await categoryRepository.TotalCategories(request.NameFilter, request.LangCode);
        return new PaginationResponse<CategoryPaginationResponseDto>(categories, total, request.PageSize, request.PageNumber);
    }
}
