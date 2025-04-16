using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.GetCategory;

public record GetCategoryByNameQuery(string name) : IQuery<GetCategoryRespone>;

public sealed class GetCategoryByNameQueryHandler(ICategoryRepository categoryRepository) : IQueryHandler<GetCategoryByNameQuery, GetCategoryRespone>
{
    public async Task<Result<GetCategoryRespone>> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetByIdAsync(request.name);
        if (category is null)
            return new CategoryNotFoundException(request.name);
        return new GetCategoryRespone(category);
    }
}

internal class GetCategoryByNameQueryValidator : AbstractValidator<GetCategoryByNameQuery>
{
    public GetCategoryByNameQueryValidator()
    {
        RuleFor(c => c.name).NotEmpty();
    }
}


public class GetCategoryRespone
{
    public GetCategoryRespone(Category category)
    {
        this.categoryName = category.CategoryName;
        this.categoryPath = category.CategoryPath;
        this.description = category.Description;
        this.order = category.Order;
        this.imageUrl = category.ImageUrl;

        this.parent = category.ParentCategory is null ? null : new OrphanCategory()
        {
            categoryName = category.ParentCategory.CategoryName,
            categoryPath = category.ParentCategory.CategoryPath,
            description = category.ParentCategory.Description,
            imageUrl = category.ParentCategory.ImageUrl,
            order = category.ParentCategory.Order
        };

        this.specifications = category.CategorySpecs.Select(x => x.Spec).Select(x => new Specification
        {
            name = x.Name,
            id = x.Id,
            dataType = x.DataType,
            options = x.SpecificationOptions.Select(so => new SpecOptionResponse(so.Id, so.SpecificationId, so.Value)).ToList()
        }).ToList();

        this.children = category.ChilrenCategories.Select(x => new OrphanCategory
        {
            categoryName = x.CategoryName,
            description = x.Description,
            order = x.Order,
            imageUrl = x.ImageUrl,
            categoryPath = x.CategoryPath
        }).ToList();
    }
    public string categoryName { get; private set; } = string.Empty;
    public string description { get; private set; } = string.Empty;
    public int order { get; private set; }
    public string? imageUrl { get; private set; }
    public string categoryPath { get; private set; } = string.Empty;
    public ICollection<Specification> specifications { get; set; } = [];
    public OrphanCategory? parent { get; init; }
    public ICollection<OrphanCategory> children { get; private set; } = [];
    public class OrphanCategory
    {
        public string categoryName { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int order { get; set; }
        public string? imageUrl { get; set; }
        public string categoryPath { get; set; } = string.Empty;
    }
    public class Specification
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public string dataType { get; set; } = string.Empty;
        public ICollection<SpecOptionResponse> options { get; set; } = [];
    }
    public record SpecOptionResponse(Guid Id, Guid SpecificationId, string Value);
}
