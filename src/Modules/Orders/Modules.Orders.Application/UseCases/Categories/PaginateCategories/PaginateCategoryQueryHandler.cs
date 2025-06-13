using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Application.Repositories;

namespace Modules.Orders.Application.UseCases.PaginateCategories;

public sealed class PaginateCategoryQueryHandler(ICategoryRepository categoryRepository) : IQueryHandler<PaginateCategoryQuery, PaginationResponse<CategoryPaginationResponseDto>>
{
    public async Task<Result<PaginationResponse<CategoryPaginationResponseDto>>> Handle(PaginateCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.Paginate(request.pageNumber, request.pageSize, request.nameFilter, request.lang_code);
        var total = await categoryRepository.TotalCategories(request.nameFilter, request.lang_code);
        return new PaginationResponse<CategoryPaginationResponseDto>(categories, total, request.pageSize, request.pageNumber);
    }
}
