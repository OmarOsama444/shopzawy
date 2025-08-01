using Common.Domain;
using Common.Application.Messaging;
using Modules.Catalog.Application.Dtos;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Exceptions;

namespace Modules.Catalog.Application.UseCases.Categories.GetCategory;

public sealed class GetCategoryByIdQueryHandler(
    ICategoryRepository categoryRepository,
    ISpecStatisticRepository specStatisitcRepository,
    ISpecRepository specRepository) :
    IQueryHandler<GetCategoryByIdQuery, CategoryResponeDto>
{
    public async Task<Result<CategoryResponeDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetById(request.Id, request.LangCode);
        if (category == null)
            return new CategoryNotFoundException(request.Id);
        var parent = await categoryRepository.GetParentById(request.Id, request.LangCode);
        var children = await categoryRepository.GetChildrenById(request.Id, request.LangCode);
        var categoryPath = await categoryRepository.GetCategoryPath(request.Id, request.LangCode);
        var specsStat = await specStatisitcRepository.GetByCategoryId(category.Id, [.. category.Path, category.Id], request.LangCode);
        var specs = await specRepository.GetByCategoryId(category.Id, [.. category.Path, category.Id], request.LangCode);
        return new CategoryResponeDto()
        {
            Current = category,
            CategoryPath = categoryPath,
            Parent = parent,
            Children = children,
            Specifications = specs,
            SpecificationsCount = specsStat
        };
    }
}

