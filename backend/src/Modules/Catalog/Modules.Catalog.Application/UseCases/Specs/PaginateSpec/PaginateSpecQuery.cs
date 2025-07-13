using Common.Application.Messaging;
using Common.Domain.ValueObjects;
using Modules.Catalog.Application.Dtos;

namespace Modules.Catalog.Application.UseCases.Specs.PaginateSpec;

public record PaginateSpecQuery(int PageNumber, int PageSize, string? Name, Language LangCode) : IQuery<PaginationResponse<TranslatedSpecResponseDto>>;
