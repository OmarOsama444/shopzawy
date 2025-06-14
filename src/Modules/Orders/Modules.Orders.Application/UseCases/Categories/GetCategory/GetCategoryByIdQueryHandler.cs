using Modules.Common.Domain;
using Modules.Common.Application.Messaging;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Application.UseCases.Categories.Dtos;
using Modules.Orders.Application.Repositories;

namespace Modules.Orders.Application.UseCases.Categories.GetCategory;

public sealed class GetCategoryByIdQueryHandler(
    ICategoryRepository categoryRepository,
    ISpecRepository specRepository) :
    IQueryHandler<GetCategoryByIdQuery, CategoryResponeDto>
{
    public async Task<Result<CategoryResponeDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetById(request.Id, request.LangCode);
        if (category is null)
            return new CategoryNotFoundException(request.Id);
        var parent = await categoryRepository.GetParentById(request.Id, request.LangCode);
        var children = await categoryRepository.GetChildrenById(request.Id, request.LangCode);
        var categoryPath = await categoryRepository.GetCategoryPath(request.Id, request.LangCode);
        var specs = await specRepository.GetByCategoryId(request.LangCode, categoryPath.Keys.ToArray());
        return new CategoryResponeDto()
        {
            Current = category,
            CategoryPath = categoryPath,
            Parent = parent,
            Children = children,
            Specifications = specs
        };
    }
}

