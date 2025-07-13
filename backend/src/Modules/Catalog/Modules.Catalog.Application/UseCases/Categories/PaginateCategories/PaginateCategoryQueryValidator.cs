using FluentValidation;

namespace Modules.Catalog.Application.UseCases.Categories.PaginateCategories;

internal class PaginateCategoryQueryValidator : AbstractValidator<PaginateCategoryQuery>
{
    public PaginateCategoryQueryValidator()
    {
        RuleFor(p => p.PageNumber).NotEmpty().GreaterThan(0);
        RuleFor(p => p.PageSize).NotEmpty().InclusiveBetween(1, 50);
        RuleFor(p => p.LangCode).NotEmpty();
    }
}