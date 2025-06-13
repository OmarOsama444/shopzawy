using System.Data.Common;
using FluentValidation;
using Modules.Common.Domain;
using Modules.Common.Application.Messaging;
using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Application.Services;
using Modules.Orders.Application.Abstractions;
using Modules.Common.Infrastructure;
using Dapper;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Application.UseCases.Categories.Dtos;
using Modules.Orders.Application.Repositories;

namespace Modules.Orders.Application.UseCases.Categories.GetCategory;

public record GetCategoryByIdQuery(Guid id, Language lang_code) : IQuery<CategoryResponeDto>;

public sealed class GetCategoryByIdQueryHandler(
    ICategoryRepository categoryRepository,
    ISpecRepository specRepository) :
    IQueryHandler<GetCategoryByIdQuery, CategoryResponeDto>
{
    public async Task<Result<CategoryResponeDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetById(request.id, request.lang_code);
        if (category is null)
            return new CategoryNotFoundException(request.id);
        var parent = await categoryRepository.GetParentById(request.id, request.lang_code);
        var children = await categoryRepository.GetChildrenById(request.id, request.lang_code);
        var categoryPath = await categoryRepository.GetCategoryPath(request.id, request.lang_code);
        var specs = await specRepository.GetByCategoryId(request.lang_code, categoryPath.Keys.ToArray());
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

internal class GetCategoryByNameQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByNameQueryValidator()
    {
        RuleFor(c => c.id).NotEmpty();
        RuleFor(c => c.lang_code).NotEmpty();
    }
}

