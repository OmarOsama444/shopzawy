using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.GetMainCategories;

public record GetMainCategoriesQuery : IQuery<ICollection<CategoryResponse>>;

public sealed class GetMainCategoriesQueryHandler(ICategoryRepository categoryRepository) : IQueryHandler<GetMainCategoriesQuery, ICollection<CategoryResponse>>
{
    public async Task<Result<ICollection<CategoryResponse>>> Handle(GetMainCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetMainCategories();
        return categories.Select(
            c =>
            {
                return new CategoryResponse()
                {
                    CategoryName = c.CategoryName,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl
                };
            }
        ).ToList();
    }
}

public class CategoryResponse
{

    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}

internal class GetMainCategoriesValidator : AbstractValidator<GetMainCategoriesQuery>
{

}
