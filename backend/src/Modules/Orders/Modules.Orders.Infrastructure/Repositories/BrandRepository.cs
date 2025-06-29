using System.Data.Common;
using Common.Application;
using Common.Domain.ValueObjects;
using Common.Infrastructure;
using Dapper;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories;

public class BrandRepository(OrdersDbContext ordersDbContext, IDbConnectionFactory dbConnectionFactory) : Repository<Brand, OrdersDbContext>(ordersDbContext), IBrandRepository
{
    // todo add translation here
    public async Task<ICollection<TranslatedBrandResponseDto>> Paginate(int pageNumber, int pageSize, string? nameField, Language langCode)
    {
        if (!string.IsNullOrEmpty(nameField))
            nameField += "%";
        else
            nameField = null;
        await using DbConnection sqlConnection = await dbConnectionFactory.CreateSqlConnection();
        int offset = (pageNumber - 1) * pageSize;
        string Query =
        $"""
        SELECT
            B.id as {nameof(TranslatedBrandResponseDto.Id)} ,
            B.Logo_Url as {nameof(TranslatedBrandResponseDto.LogoUrl)} ,
            B.active as {nameof(TranslatedBrandResponseDto.Active)} ,
            B.featured as {nameof(TranslatedBrandResponseDto.Featured)} ,
            BT.name as {nameof(TranslatedBrandResponseDto.Name)} ,
            BT.description as {nameof(TranslatedBrandResponseDto.Description)}
        FROM
            {Schemas.Orders}.Brand AS B
        LEFT JOIN
            {Schemas.Orders}.Brand_Translation as BT
        ON 
            B.id = BT.Brand_id
        WHERE
            BT.lang_code = @langCode
        ORDER BY
            BT.name
        LIMIT @pageSize OFFSET @offset;
        {(nameField is null ? " ; " : " WHERE B.Brand_Name ILIKE @nameField ;")}
        """;
        IEnumerable<TranslatedBrandResponseDto> brands = await sqlConnection.QueryAsync<TranslatedBrandResponseDto>(Query, new { nameField, offset, pageSize, langCode });
        return brands.ToList();
    }

    public async Task<int> TotalBrands(string? nameField, Language langCode)
    {
        if (!string.IsNullOrEmpty(nameField))
            nameField += "%";
        else
            nameField = null;
        await using DbConnection sqlConnection = await dbConnectionFactory.CreateSqlConnection();
        string countQuery = $"""
        SELECT 
            COUNT(*) 
        FROM 
            {Schemas.Orders}.Brand as B
        LEFT JOIN
            {Schemas.Orders}.Brand_Translation as BT
        ON
            b.id = bt.Brand_id AND bt.lang_code = @langCode
        {(nameField is null ? " ; " : "WHERE bt.name ILIKE @nameField ;")}
        """;
        return await sqlConnection.ExecuteScalarAsync<int>(countQuery, new { nameField = nameField, langCode });
    }
}
