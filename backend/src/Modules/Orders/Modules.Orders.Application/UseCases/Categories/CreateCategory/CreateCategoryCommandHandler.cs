using Common.Application.Messaging;
using Common.Domain;
using Modules.Orders.Application.Services;

namespace Modules.Orders.Application.UseCases.Categories.CreateCategory;

public class CreateCategoryCommandHandler(
    ICategoryService categoryService
    ) : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await categoryService.CreateCategory(
            request.Order,
            request.ParentCategoryId,
            request.SpecIds,
            request.Names,
            request.Descriptions,
            request.ImageUrls);
    }
}
