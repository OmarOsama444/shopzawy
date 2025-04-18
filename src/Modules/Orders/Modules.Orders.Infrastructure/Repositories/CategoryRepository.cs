using System.Data.Common;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Modules.Common.Infrastructure;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class CategoryRepository(
    OrdersDbContext ordersDbContext,
    IDbConnectionFactory dbConnectionFactory
    ) : Repository<Category, OrdersDbContext>(ordersDbContext), ICategoryRepository
{
    public async Task<ICollection<Category>> Children(string CategoryName)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        WITH RECURSIVE category_hierarchy AS (
        SELECT 
            category_name ,
            description ,
            image_url ,
            parent_category_name ,
            "order" 
        FROM orders.category 
        WHERE parent_category_name = @CategoryName

        UNION ALL

        SELECT
            c.category_name,
            c.description ,
            c.image_url,
            c.category_path,
            c.parent_category_name,
            c.order
        FROM orders.category c
        INNER JOIN category_hierarchy ch ON c.parent_category_name = ch.category_name
        )

        SELECT 
            category_name as {nameof(Category.CategoryName)}, 
            description as {nameof(Category.Description)},
            image_url as {nameof(Category.ImageUrl)},
            parent_category_name as {nameof(Category.ParentCategoryName)},
            "order" as {nameof(Category.Order)}
        FROM category_hierarchy;
        """;
        IEnumerable<Category> categories = await dbConnection.QueryAsync<Category>(Query, new { CategoryName });
        return categories.ToList();
    }
    public override async Task<Category?> GetByIdAsync(object id)
    {
        string categoryName = id as string ?? "";

        return await context.Categories
            .Include(x => x.ChilrenCategories)
            .Include(x => x.ParentCategory)
            .Include(x => x.CategorySpecs)
            .ThenInclude(x => x.Specification)
            .ThenInclude(c => c.SpecificationOptions)
            .FirstOrDefaultAsync(x => x.CategoryName == categoryName);
    }

    public async Task<ICollection<string>> GetCategoryPath(string CategoryName)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        """
        WITH RECURSIVE category_path AS (
        SELECT 
            category_name,
            parent_category_name,
            1 AS Level
        FROM orders.category
        WHERE category_name = @CategoryName

        UNION ALL

        SELECT 
            c.category_name,
            c.parent_category_name,
            cp.Level + 1
        FROM 
            orders.category c
        INNER JOIN 
            category_path cp 
        ON 
            c.category_name = cp.parent_category_name
        )
        SELECT 
        category_name as value
        FROM category_path
        ORDER BY Level DESC;
        """;
        return (await dbConnection.QueryAsync<string>(Query, new { CategoryName = CategoryName })).ToList();
    }

    public async Task<ICollection<Category>> GetMainCategories()
    {
        await using DbConnection dbConnection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        SELECT 
            category_name as {nameof(Category.CategoryName)}, 
            description as {nameof(Category.Description)},
            image_url as {nameof(Category.ImageUrl)}
        FROM
        {Schemas.Orders}.Category C
        WHERE
        Parent_Category_Name is NULL
        ORDER BY 
            C.Order
        """;
        IEnumerable<Category> categories = await dbConnection.QueryAsync<Category>(Query);
        return categories.ToList();
    }

    public async Task<bool> IsLeafCategory(string CategoryName)
    {
        return !await context.Categories.AnyAsync(x => x.ParentCategoryName == CategoryName);
    }

    public async Task<ICollection<CategoryResponse>> Paginate(int pageNumber, int pageSize, string? nameFilter)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.CreateSqlConnection();
        int offset = (pageNumber - 1) * pageSize;
        string Query =
        $"""
        SELECT 
            C.Category_Name as {nameof(CategoryResponse.CategoryName)},
            C.Order as {nameof(CategoryResponse.Order)},
            C.Parent_Category_Name as {nameof(CategoryResponse.ParentName)},
            COUNT(DISTINCT CC.Category_Name) AS {nameof(CategoryResponse.NumberOfChildren)},
            COUNT(DISTINCT P.Id) AS {nameof(CategoryResponse.NumberOfProducts)}
        FROM 
            {Schemas.Orders}.Category AS C
        LEFT JOIN 
            {Schemas.Orders}.Category AS CC 
        ON 
            CC.Parent_Category_Name = C.Category_Name
        LEFT JOIN 
            {Schemas.Orders}.Product AS P ON P.Category_Name = C.Category_Name
        WHERE 
            (@nameFilter IS NULL OR C.Category_Name ILIKE @nameFilter || '%')
        GROUP BY 
            C.Category_Name, C.Order, C.Parent_Category_Name
        ORDER BY 
            C.Category_Name
        LIMIT @pageSize OFFSET @offset;
        """;
        return (await dbConnection.QueryAsync<CategoryResponse>(Query, new { offset, nameFilter, pageSize })).ToList();
    }

    public async Task<int> TotalCategories(string? nameFilter)
    {
        await using DbConnection dbConnection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        SELECT 
            COUNT(C.Category_Name)
        FROM 
            {Schemas.Orders}.Category AS C
        WHERE 
            (@filter IS NULL OR C.Category_Name ILIKE @filter || '%')
        """;
        return await dbConnection.ExecuteScalarAsync<int>(Query, new { filter = nameFilter });
    }

}