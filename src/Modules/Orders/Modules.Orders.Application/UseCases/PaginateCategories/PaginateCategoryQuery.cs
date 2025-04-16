using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.PaginateCategories;

public record PaginateCategoryQuery(
    int pageNumber,
    int pageSize,
    string? nameFilter) : IQuery<PaginationResponse<CategoryResponse>>;

public sealed class PaginateCategoryQueryHandler(ICategoryRepository categoryRepository) : IQueryHandler<PaginateCategoryQuery, PaginationResponse<CategoryResponse>>
{
    public async Task<Result<PaginationResponse<CategoryResponse>>> Handle(PaginateCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.Paginate(request.pageNumber, request.pageSize, request.nameFilter);
        var total = await categoryRepository.TotalCategories(request.nameFilter);
        return new PaginationResponse<CategoryResponse>(categories, total, request.pageSize, request.pageNumber);
    }
}

internal class PaginateCategoryQueryValidator : AbstractValidator<PaginateCategoryQuery>
{
    public PaginateCategoryQueryValidator()
    {
        RuleFor(p => p.pageNumber).NotEmpty().GreaterThan(0);
        RuleFor(p => p.pageSize).NotEmpty().GreaterThan(0).LessThan(50);
    }
}