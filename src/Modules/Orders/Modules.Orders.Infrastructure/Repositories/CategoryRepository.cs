using System.Data.Common;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Modules.Common.Infrastructure;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class CategoryRepository(
    OrdersDbContext ordersDbContext,
    IDbConnectionFactory dbConnectionFactory
    ) : Repository<Category, OrdersDbContext>(ordersDbContext), ICategoryRepository
{
    public void AddTranslation(CategoryTranslation categoryTranslation)
    {
        context.CategoryTranslations.Add(categoryTranslation);
    }

    public async Task<ICollection<MainCategoryResponse>> Children(Guid Id, Language lang_code)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        WITH RECURSIVE category_hierarchy AS (
        SELECT 
            id ,
            parent_category_id ,
            "order" 
        FROM orders.category 
        WHERE parent_category_id = @Id

        UNION ALL

        SELECT
            c.id,
            c.parent_category_id,
            c.order
        FROM 
            orders.category c
        INNER JOIN 
            category_hierarchy ch 
        ON 
            c.parent_category_id = ch.id
        )

        SELECT 
            CH.id as {nameof(MainCategoryResponse.Id)} ,
            CH."order" as {nameof(MainCategoryResponse.order)} ,
            CH.parent_category_id as {nameof(MainCategoryResponse.parentCategoryId)} ,
            CT.description as {nameof(MainCategoryResponse.Description)} ,
            CT.image_url as {nameof(MainCategoryResponse.ImageUrl)} ,
            CT.name as {nameof(MainCategoryResponse.CategoryName)}
        FROM 
            category_hierarchy AS CH
        LEFT JOIN
            category_translation AS CT
        ON
            CH.id = CT.category_id
        WHERE
            CT.lang_code = @lang_code
        """;
        IEnumerable<MainCategoryResponse> categories = await dbConnection.QueryAsync<MainCategoryResponse>(Query, new { Id, lang_code });
        return categories.ToList();
    }
    public override async Task<Category?> GetByIdAsync(object id)
    {
        Guid categoryId = (Guid)id;

        return await context.Categories
            .Include(x => x.ChilrenCategories)
            .Include(x => x.ParentCategory)
            .Include(x => x.CategorySpecs)
            .ThenInclude(x => x.Specification)
            .ThenInclude(c => c.SpecificationOptions)
            .FirstOrDefaultAsync(x => x.Id == categoryId);
    }

    public async Task<IDictionary<Guid, string>> GetCategoryPath(Guid Id, Language LangCode)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        WITH RECURSIVE category_path AS (
        SELECT 
            id,
            parent_category_id,
            1 AS Level
        FROM {Schemas.Orders}.category
        WHERE id = @Id

        UNION ALL

        SELECT 
            c.id,
            c.parent_category_id,
            cp.Level + 1
        FROM 
            {Schemas.Orders}.category c
        INNER JOIN 
            category_path cp 
        ON 
            c.id = cp.parent_category_id
        )
        SELECT 
            CP.id as key
            CT.name as value
        FROM 
            category_path AS CP
        LEFT JOIN
            {Schemas.Orders}.category_translation AS CT
        ON
            CP.id = CT.category_id
        AND
            CT.lang_code = @LangCode
        ORDER BY Level DESC;
        """;
        return (await dbConnection.QueryAsync<(Guid key, string value)>(Query, new { Id, LangCode })).ToDictionary();
    }

    public async Task<ICollection<MainCategoryResponse>> GetMainCategories(Language lang_code)
    {
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        SELECT
            C.id AS {nameof(MainCategoryResponse.Id)} , 
            C.parent_category_id AS {nameof(MainCategoryResponse.parentCategoryId)} ,
            C.order AS {nameof(MainCategoryResponse.order)} ,
            CT.name AS {nameof(MainCategoryResponse.CategoryName)} ,
            CT.description AS {nameof(MainCategoryResponse.Description)} ,
            CT.Image_url AS {nameof(MainCategoryResponse.ImageUrl)} 
        FROM
            {Schemas.Orders}.category AS C
        LEFT JOIN
            {Schemas.Orders}.category_translation AS CT
        ON
            C.id = Ct.Category_id
        WHERE
            CT.lang_code = @lang_code
        """;

        IEnumerable<MainCategoryResponse> mainCategoryResponses =
            await connection.QueryAsync<MainCategoryResponse>(Query, new { lang_code });
        return mainCategoryResponses.ToList();
    }

    public async Task<bool> IsLeafCategory(Guid Id)
    {
        return !await context.Categories.AnyAsync(x => x.ParentCategoryId == Id);
    }

    public async Task<ICollection<CategoryPaginationResponse>> Paginate(int pageNumber, int pageSize, string? nameFilter, Language langCode)
    {
        if (!string.IsNullOrEmpty(nameFilter))
            nameFilter = $"{nameFilter}%";
        await using DbConnection dbConnection = await dbConnectionFactory.CreateSqlConnection();
        int offset = (pageNumber - 1) * pageSize;
        string Query =
        $"""
        SELECT 
            C.id as {nameof(CategoryPaginationResponse.Id)},
            CT.name as {nameof(CategoryPaginationResponse.CategoryName)},
            C.Order as {nameof(CategoryPaginationResponse.Order)},
            C.parent_category_id as {nameof(CategoryPaginationResponse.parentCategoryId)} ,
            COUNT(DISTINCT CC.id) AS {nameof(CategoryPaginationResponse.NumberOfChildren)},
            COUNT(DISTINCT P.Id) AS {nameof(CategoryPaginationResponse.NumberOfProducts)}
        FROM 
            {Schemas.Orders}.Category AS C
        LEFT JOIN 
            {Schemas.Orders}.Category AS CC 
        ON 
            CC.parent_category_id = C.id
        LEFT JOIN 
            {Schemas.Orders}.Product AS P 
        ON 
            P.category_id = C.id
        LEFT JOIN
            {Schemas.Orders}.category_translation AS CT
        ON
            CT.category_id = C.id AND CT.lang_code = @langCode
        WHERE 
            (@nameFilter IS NULL OR CT.name ILIKE @nameFilter )
        GROUP BY 
            C.id , C.Order, C.Parent_Category_id , CT.name
        ORDER BY 
            C.order , C.id
        LIMIT 
            @pageSize 
        OFFSET 
            @offset;
        """;
        var results = await dbConnection
            .QueryAsync<CategoryPaginationResponse>(
                Query, new
                {
                    offset,
                    nameFilter,
                    pageSize,
                    langCode
                });
        return results.ToList();
    }

    public async Task<int> TotalCategories(string? nameFilter, Language langCode)
    {
        if (!string.IsNullOrEmpty(nameFilter))
            nameFilter = $"{nameFilter}%";
        await using DbConnection dbConnection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        SELECT
            COUNT(*)
        FROM 
            {Schemas.Orders}.category_translation AS CT
        WHERE
            CT.lang_code = @langCode
        AND
            (@filter IS NULL OR CT.name ILIKE @filter )
        """;
        return await dbConnection.ExecuteScalarAsync<int>(Query, new { filter = nameFilter, langCode });
    }

}