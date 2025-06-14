using FluentValidation;

namespace Modules.Orders.Application.UseCases.Categories.GetCategory;

internal class GetCategoryByNameQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByNameQueryValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.LangCode).NotEmpty();
    }
}

