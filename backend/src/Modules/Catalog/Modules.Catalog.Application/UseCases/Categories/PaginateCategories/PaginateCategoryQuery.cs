using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Modules.Catalog.Application.Dtos;

namespace Modules.Catalog.Application.UseCases.Categories.PaginateCategories;

public record PaginateCategoryQuery(
    int PageNumber,
    int PageSize,
    string? NameFilter,
    Language LangCode) : IQuery<PaginationResponse<CategoryPaginationResponseDto>>;
