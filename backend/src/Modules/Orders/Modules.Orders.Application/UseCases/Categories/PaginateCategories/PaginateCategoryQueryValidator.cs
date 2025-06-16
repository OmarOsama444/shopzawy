using FluentValidation;

namespace Modules.Orders.Application.UseCases.PaginateCategories;

internal class PaginateCategoryQueryValidator : AbstractValidator<PaginateCategoryQuery>
{
    public PaginateCategoryQueryValidator()
    {
        RuleFor(p => p.pageNumber).NotEmpty().GreaterThan(0);
        RuleFor(p => p.pageSize).NotEmpty().InclusiveBetween(1, 50);
        RuleFor(p => p.lang_code).NotEmpty();
    }
}