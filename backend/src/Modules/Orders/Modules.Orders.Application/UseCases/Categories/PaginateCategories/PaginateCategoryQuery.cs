using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.PaginateCategories;

public record PaginateCategoryQuery(
    int PageNumber,
    int PageSize,
    string? NameFilter,
    Language LangCode) : IQuery<PaginationResponse<CategoryPaginationResponseDto>>;
