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
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.UseCases.Categories.GetCategory;

public record GetCategoryByIdQuery(Guid id, Language lang_code) : IQuery<CategoryRespone>;

public sealed class GetCategoryByIdQueryHandler(
    IDbConnectionFactory dbConnectionFactory,
    ICategoryRepository categoryRepository) :
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
            CT.lang_code = @lang_code && C.id = @id ;
    
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
            CT.lang_code = @lang_code && C.parent_id = @id ;

        SELECT
            PC.id as {nameof(CategoryRespone.SubCategory.Id)} ,
            PCT.name as {nameof(CategoryRespone.SubCategory.categoryName)} ,
            PCT.description as {nameof(CategoryRespone.SubCategory.description)} ,
            PC."order" as {nameof(CategoryRespone.SubCategory.order)} ,
            PCT.image_url as {nameof(CategoryRespone.SubCategory.imageUrl)}
        FROM 
            {Schemas.Orders}.category as C
        JOIN 
            {Schemas.Orders}.category as PC ON C.parent_id = PC.id
        LEFT JOIN 
            {Schemas.Orders}.category_translation as PCT ON PC.id = PCT.Category_Id
        WHERE 
            C.id = @id AND PCT.lang_code = @lang_code;

        SELECT
            s.id as id , st.name , s.data_type as dataType ,
            so.id as optionId , so.value as value 
        FROM
            {Schemas.Orders}.category_spec as cs
        LEFT JOIN
            {Schemas.Orders}.specification as s
        ON
            cs.spec_id = s.id
        LEFT JOIN
            {Schemas.Orders}.specification_option as so
        ON
            so.specification_id = s.id
        LEFT JOIN
            {Schemas.Orders}.specification_translation as st
        ON
            s.id = st.spec_id
        WHERE
            st.lang_code = @lang_code AND cs.category_id = @id
        """;

        var multiSelect = await connection.QueryMultipleAsync(Query, request);
        var category = await multiSelect.ReadFirstOrDefaultAsync<CategoryRespone.SubCategory>();
        if (category == null)
            return new CategoryNotFoundException(request.id);
        var childrenCategory = multiSelect.Read<CategoryRespone.SubCategory>();
        var parentCategory = multiSelect.ReadFirstOrDefault();

        var specs = multiSelect.Read<
            CategoryRespone.SpecResponse,
            CategoryRespone.SpecOptionResponse,
            CategoryRespone.SpecResponse>(
            (spec, option) =>
            {
                if (spec.options is null || spec.options.Count == 0)
                    spec.options = new List<CategoryRespone.SpecOptionResponse>();
                spec.options.Add(option);
                return spec;
            },
            splitOn: "optionId").GroupBy(s => s.id)
            .Select(g =>
            {
                var spec = g.First();
                spec.options = g.SelectMany(s => s.options).ToList();
                return spec;
            }).ToList();
        return new CategoryRespone()
        {
            Id = category.Id,
            categoryName = category.categoryName,
            description = category.description,
            order = category.order,
            imageUrl = category.imageUrl,
            categoryPath = await categoryRepository.GetCategoryPath(request.id, request.lang_code),
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

