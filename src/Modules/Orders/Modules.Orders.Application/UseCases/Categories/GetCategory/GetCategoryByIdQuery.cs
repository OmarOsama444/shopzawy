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
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.Categories.GetCategory;

public record GetCategoryByIdQuery(Guid id, Language lang_code) : IQuery<CategoryRespone>;

public sealed class GetCategoryByIdQueryHandler(
    IDbConnectionFactory dbConnectionFactory,
    ICategoryRepository categoryRepository,
    ISpecRepository specRepository) :
    IQueryHandler<GetCategoryByIdQuery, CategoryRespone>
{
    public async Task<Result<CategoryRespone>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        SELECT
            C.id as {nameof(CategoryRespone.SubCategory.Id)} ,
            CT.name as {nameof(CategoryRespone.SubCategory.categoryName)} ,
            CT.description as {nameof(CategoryRespone.SubCategory.description)} ,
            C."order" as {nameof(CategoryRespone.SubCategory.order)} ,
            CT.image_url as {nameof(CategoryRespone.SubCategory.imageUrl)}
        FROM 
            {Schemas.Orders}.category as C 
        LEFT JOIN
            {Schemas.Orders}.category_translation as CT
        ON
            C.id = CT.Category_Id
        WHERE
            CT.lang_code = @lang_code AND C.id = @id ;
    
        SELECT
            C.id as {nameof(CategoryRespone.SubCategory.Id)} ,
            CT.name as {nameof(CategoryRespone.SubCategory.categoryName)} ,
            CT.description as {nameof(CategoryRespone.SubCategory.description)} ,
            C."order" as {nameof(CategoryRespone.SubCategory.order)} ,
            CT.image_url as {nameof(CategoryRespone.SubCategory.imageUrl)}
        FROM 
            {Schemas.Orders}.category as C 
        LEFT JOIN
            {Schemas.Orders}.category_translation as CT
        ON
            C.id = CT.Category_Id
        WHERE
            CT.lang_code = @lang_code AND C.parent_category_id = @id ;

        SELECT
            PC.id as {nameof(CategoryRespone.SubCategory.Id)} ,
            PCT.name as {nameof(CategoryRespone.SubCategory.categoryName)} ,
            PCT.description as {nameof(CategoryRespone.SubCategory.description)} ,
            PC."order" as {nameof(CategoryRespone.SubCategory.order)} ,
            PCT.image_url as {nameof(CategoryRespone.SubCategory.imageUrl)}
        FROM 
            {Schemas.Orders}.category as C
        JOIN 
            {Schemas.Orders}.category as PC ON C.parent_category_id = PC.id
        LEFT JOIN 
            {Schemas.Orders}.category_translation as PCT ON PC.id = PCT.Category_Id
        WHERE 
            C.id = @id AND PCT.lang_code = @lang_code;
        """;

        var multiSelect = await connection.QueryMultipleAsync(Query, request);
        var category = await multiSelect.ReadFirstOrDefaultAsync<CategoryRespone.SubCategory>();
        if (category == null)
            return new CategoryNotFoundException(request.id);
        var childrenCategory = await multiSelect.ReadAsync<CategoryRespone.SubCategory>();
        var parentCategory = await multiSelect.ReadFirstOrDefaultAsync<CategoryRespone.SubCategory>();
        var categoryPath = await categoryRepository.GetCategoryPath(request.id, request.lang_code);
        var specs = await specRepository.GetByCategoryId(request.lang_code, categoryPath.Keys.ToArray());
        return new CategoryRespone()
        {
            Id = category.Id,
            categoryName = category.categoryName,
            description = category.description,
            order = category.order,
            imageUrl = category.imageUrl,
            categoryPath = categoryPath,
            parent = parentCategory,
            children = childrenCategory.ToList(),
            specifications = specs
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

