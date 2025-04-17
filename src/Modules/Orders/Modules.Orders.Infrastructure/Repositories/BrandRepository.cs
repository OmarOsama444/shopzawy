using System.Data.Common;
using Dapper;
using Modules.Common.Infrastructure;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class BrandRepository(OrdersDbContext ordersDbContext, IDbConnectionFactory dbConnectionFactory) : Repository<Brand, OrdersDbContext>(ordersDbContext), IBrandRepository
{
    public async Task<ICollection<BrandResponse>> Paginate(int pageNumber, int pageSize, string? nameField)
    {
        await using DbConnection sqlConnection = await dbConnectionFactory.CreateSqlConnection();
        int offset = (pageNumber - 1) * pageSize;
        string Query =
        @"
        SELECT
            B.Brand_Name as BrandName ,
            B.Logo_Url as LogoUrl,
            B.Description as Description,
            B.Featured as Featured,
            B.Active as Active ,
            COUNT(P.Id) AS NumberOfProducts
        FROM
            Orders.Brand AS B
        LEFT JOIN
            Orders.Product AS P
        ON P.Brand_Name = B.Brand_Name
        WHERE
            (@nameField IS NULL OR B.Brand_Name ILIKE @nameField || '%')
        GROUP BY
            B.Brand_Name, B.Logo_Url, B.Description, B.Featured, B.Active
        ORDER BY
            B.Brand_Name
        LIMIT @pageSize OFFSET @offset;
        ";
        IEnumerable<BrandResponse> brands = await sqlConnection.QueryAsync<BrandResponse>(Query, new { nameField, offset, pageSize });
        return brands.ToList();
    }

    public async Task<int> TotalBrands(string? nameField)
    {

        await using DbConnection sqlConnection = await dbConnectionFactory.CreateSqlConnection();
        string countQuery = $"""
        SELECT 
            COUNT(*) 
        FROM 
            {Schemas.Orders}.Brand as B
        WHERE 
            (@nameField IS NULL OR B.Brand_Name ILIKE @nameField || '%')
        """;
        return await sqlConnection.ExecuteScalarAsync<int>(countQuery, new { nameField = nameField });
    }
}
