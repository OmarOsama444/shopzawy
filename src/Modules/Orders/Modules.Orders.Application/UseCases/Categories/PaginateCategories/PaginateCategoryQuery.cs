using Modules.Common.Application.Messaging;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.PaginateCategories;

public record PaginateCategoryQuery(
    int pageNumber,
    int pageSize,
    string? nameFilter,
    Language lang_code) : IQuery<PaginationResponse<CategoryPaginationResponseDto>>;
