using Common.Application.Messaging;
using Common.Domain;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Application.Repositories;

namespace Modules.Orders.Application.UseCases.GetMainCategories;

public sealed class GetMainCategoriesQueryHandler(ICategoryRepository categoryRepository) : IQueryHandler<GetMainCategoriesQuery, ICollection<TranslatedCategoryResponseDto>>
{
    public async Task<Result<ICollection<TranslatedCategoryResponseDto>>> Handle(
        GetMainCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetMainCategories(request.lang_code);
        return categories.ToList();
    }
}
