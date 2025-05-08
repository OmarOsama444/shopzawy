using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.PaginateCategories;

public record PaginateCategoryQuery(
    int pageNumber,
    int pageSize,
    string? nameFilter,
    Language lang_code) : IQuery<PaginationResponse<CategoryResponse>>;

public sealed class PaginateCategoryQueryHandler(ICategoryRepository categoryRepository) : IQueryHandler<PaginateCategoryQuery, PaginationResponse<CategoryResponse>>
{
    public async Task<Result<PaginationResponse<CategoryResponse>>> Handle(PaginateCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.Paginate(request.pageNumber, request.pageSize, request.nameFilter, request.lang_code.ToString());
        var total = await categoryRepository.TotalCategories(request.nameFilter, request.lang_code.ToString());
        return new PaginationResponse<CategoryResponse>(categories, total, request.pageSize, request.pageNumber);
    }
}

internal class PaginateCategoryQueryValidator : AbstractValidator<PaginateCategoryQuery>
{
    public PaginateCategoryQueryValidator()
    {
        RuleFor(p => p.pageNumber).NotEmpty().GreaterThan(0);
        RuleFor(p => p.pageSize).NotEmpty().InclusiveBetween(1, 50);
        RuleFor(p => p.lang_code).NotEmpty();
    }
}