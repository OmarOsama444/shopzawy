using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Spec.PaginateSpec;

public record PaginateSpecQuery(int PageNumber, int PageSize, string? Name, Language LangCode) : IQuery<PaginationResponse<SpecResponse>>;
