using System.Data.Common;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        SELECT 
        *
        FROM 
        {Schemas.Orders}.Category
        WHERE
        Parent_Category_Name = @ParentName
        """;
        IEnumerable<Category> categories = await dbConnection.QueryAsync<Category>(Query, new { ParentName = CategoryName });
        return categories.ToList();
    }
    public override async Task<Category?> GetByIdAsync(object id)
    {
        string categoryName = id as string ?? "";

        return await ordersDbContext.Categories
            .Include(x => x.ChilrenCategories)
            .Include(x => x.ParentCategory)
            .Include(x => x.CategorySpecs)
            .ThenInclude(x => x.Spec)
            .FirstOrDefaultAsync(x => x.CategoryName == categoryName);
    }

    public async Task<ICollection<Category>> GetMainCategories()
    {
        await using DbConnection dbConnection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        SELECT 
        *
        FROM
        {Schemas.Orders}.Category
        WHERE
        Parent_Category_Name is NULL
        """;
        IEnumerable<Category> categories = await dbConnection.QueryAsync<Category>(Query);
        return categories.ToList();
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
            (@nameFilter IS NULL OR C.Category_Name ILIKE '%' || @nameFilter || '%')
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
            (@filter IS NULL OR C.Category_Name ILIKE '%' || @filter || '%')
        """;
        return await dbConnection.ExecuteScalarAsync<int>(Query, new { filter = nameFilter });
    }
}