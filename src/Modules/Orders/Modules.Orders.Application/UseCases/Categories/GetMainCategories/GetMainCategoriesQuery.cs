using FluentValidation;
using MassTransit;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.GetMainCategories;

public record GetMainCategoriesQuery(Language lang_code) : IQuery<ICollection<MainCategoryResponse>>;

public sealed class GetMainCategoriesQueryHandler(ICategoryRepository categoryRepository) : IQueryHandler<GetMainCategoriesQuery, ICollection<MainCategoryResponse>>
{
    public async Task<Result<ICollection<MainCategoryResponse>>> Handle(GetMainCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetMainCategories(request.lang_code);
        return categories.ToList();
    }
}


internal class GetMainCategoriesValidator : AbstractValidator<GetMainCategoriesQuery>
{
    public GetMainCategoriesValidator()
    {
        RuleFor(x => x.lang_code).NotEmpty();
    }
}
