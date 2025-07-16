using Common.Application.Messaging;
using Common.Domain;
using Modules.Catalog.Application.Services;

namespace Modules.Catalog.Application.UseCases.Categories.CreateCategory;

public class CreateCategoryCommandHandler(
    ICategoryService categoryService
    ) : ICommandHandler<CreateCategoryCommand, int>
{
    public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await categoryService.CreateCategory(
            request.Order,
            request.ParentCategoryId,
            request.SpecIds,
            request.Names,
            request.Descriptions,
            request.ImageUrls
        );
    }
}
