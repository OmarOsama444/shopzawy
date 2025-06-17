using System.Data.Common;
using Common.Application;
using Common.Domain.ValueObjects;
using Common.Infrastructure;
using Dapper;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Infrastructure.Data;

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

    public async Task<TranslatedCategoryResponseDto?> GetById(Guid id, Language langCode)
    {
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        SELECT
            C.id as {nameof(TranslatedCategoryResponseDto.Id)} ,
            CT.name as {nameof(TranslatedCategoryResponseDto.CategoryName)} ,
            CT.description as {nameof(TranslatedCategoryResponseDto.Description)} ,
            C."order" as {nameof(TranslatedCategoryResponseDto.Order)} ,
            CT.image_url as {nameof(TranslatedCategoryResponseDto.ImageUrl)}
        FROM 
            {Schemas.Orders}.category as C 
        LEFT JOIN
            {Schemas.Orders}.category_translation as CT
        ON
            C.id = CT.Category_Id
        WHERE
            CT.lang_code = @lang_code AND C.id = @id ;
        """;
        return await connection.QueryFirstOrDefaultAsync(Query, new { id, lang_code = langCode });
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

    public async Task<ICollection<TranslatedCategoryResponseDto>> GetChildrenById(Guid Id, Language langCode)
    {
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        SELECT
            PC.id as {nameof(TranslatedCategoryResponseDto.Id)} ,
            PCT.name as {nameof(TranslatedCategoryResponseDto.CategoryName)} ,
            PCT.description as {nameof(TranslatedCategoryResponseDto.Description)} ,
            PC."order" as {nameof(TranslatedCategoryResponseDto.Order)} ,
            PCT.image_url as {nameof(TranslatedCategoryResponseDto.ImageUrl)}
        FROM 
            {Schemas.Orders}.category as C
        JOIN 
            {Schemas.Orders}.category as PC ON C.parent_category_id = PC.id
        LEFT JOIN 
            {Schemas.Orders}.category_translation as PCT ON PC.id = PCT.Category_Id
        WHERE 
            C.id = @id AND PCT.lang_code = @lang_code;
        """;

        var categories = await connection.QueryAsync<TranslatedCategoryResponseDto>(Query, new
        {
            id = Id,
            lang_code = langCode
        });
        return categories.ToList();

    }

    public async Task<ICollection<TranslatedCategoryResponseDto>> GetMainCategories(Language lang_code)
    {
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        SELECT
            C.id AS {nameof(TranslatedCategoryResponseDto.Id)} , 
            C.parent_category_id AS {nameof(TranslatedCategoryResponseDto.parentCategoryId)} ,
            C.order AS {nameof(TranslatedCategoryResponseDto.Order)} ,
            CT.name AS {nameof(TranslatedCategoryResponseDto.CategoryName)} ,
            CT.description AS {nameof(TranslatedCategoryResponseDto.Description)} ,
            CT.Image_url AS {nameof(TranslatedCategoryResponseDto.ImageUrl)} 
        FROM
            {Schemas.Orders}.category AS C
        LEFT JOIN
            {Schemas.Orders}.category_translation AS CT
        ON
            C.id = Ct.Category_id
        WHERE
            CT.lang_code = @lang_code
        """;

        IEnumerable<TranslatedCategoryResponseDto> mainCategoryResponses =
            await connection.QueryAsync<TranslatedCategoryResponseDto>(Query, new { lang_code });
        return mainCategoryResponses.ToList();
    }

    public async Task<TranslatedCategoryResponseDto?> GetParentById(Guid id, Language langCode)
    {
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        SELECT
            PC.id as {nameof(TranslatedCategoryResponseDto.Id)} ,
            PCT.name as {nameof(TranslatedCategoryResponseDto.CategoryName)} ,
            PCT.description as {nameof(TranslatedCategoryResponseDto.Description)} ,
            PC."order" as {nameof(TranslatedCategoryResponseDto.Order)} ,
            PCT.image_url as {nameof(TranslatedCategoryResponseDto.ImageUrl)}
        FROM 
            {Schemas.Orders}.category as C
        JOIN 
            {Schemas.Orders}.category as PC ON C.parent_category_id = PC.id
        LEFT JOIN 
            {Schemas.Orders}.category_translation as PCT ON PC.id = PCT.Category_Id
        WHERE 
            C.id = @id AND PCT.lang_code = @lang_code;
        """;

        return await connection.QueryFirstOrDefaultAsync<TranslatedCategoryResponseDto>(Query, new
        {
            id = id,
            lang_code = langCode
        });
    }

    public async Task<ICollection<CategoryPaginationResponseDto>> Paginate(int pageNumber, int pageSize, string? nameFilter, Language langCode)
    {
        if (!string.IsNullOrEmpty(nameFilter))
            nameFilter = $"{nameFilter}%";
        await using DbConnection dbConnection = await dbConnectionFactory.CreateSqlConnection();
        int offset = (pageNumber - 1) * pageSize;
        string Query =
        $"""
        SELECT 
            C.id as {nameof(CategoryPaginationResponseDto.Id)},
            CT.name as {nameof(CategoryPaginationResponseDto.CategoryName)},
            C.Order as {nameof(CategoryPaginationResponseDto.Order)},
            C.parent_category_id as {nameof(CategoryPaginationResponseDto.parentCategoryId)} ,
            COUNT(DISTINCT CC.id) AS {nameof(CategoryPaginationResponseDto.NumberOfChildren)},
            COUNT(DISTINCT P.Id) AS {nameof(CategoryPaginationResponseDto.NumberOfProducts)}
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
            .QueryAsync<CategoryPaginationResponseDto>(
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