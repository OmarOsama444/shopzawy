using FluentValidation;

namespace Modules.Orders.Application.UseCases.GetMainCategories;

internal class GetMainCategoriesValidator : AbstractValidator<GetMainCategoriesQuery>
{
    public GetMainCategoriesValidator()
    {
        RuleFor(x => x.lang_code).NotEmpty();
    }
}
