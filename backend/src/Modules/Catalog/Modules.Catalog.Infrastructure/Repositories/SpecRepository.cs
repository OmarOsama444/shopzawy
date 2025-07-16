using System.Data.Common;
using Common.Application;
using Common.Domain.ValueObjects;
using Common.Infrastructure;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Dtos;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Domain.ValueObjects;
using Modules.Catalog.Infrastructure.Data;

namespace Modules.Catalog.Infrastructure.Repositories;

public class SpecRepository(OrdersDbContext ordersDbContext, IDbConnectionFactory dbConnectionFactory) :
    Repository<Specification, OrdersDbContext>(ordersDbContext),
    ISpecRepository
{
    public async Task<ICollection<Specification>> GetByDataType(SpecDataType dataType)
    {
        return await context.Specifications.Where(s => s.DataType == dataType).ToListAsync();
    }
    public async Task<ICollection<TranslatedSpecResponseDto>> Paginate(int pageNumber, int pageSize, string? name, Language lang_code)
    {
        if (!string.IsNullOrEmpty(name))
            name += "%";
        else
            name = null;
        int offset = ((pageNumber - 1) * pageSize);
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string query =
        $"""
        SELECT
            s.id as {nameof(TranslatedSpecResponseDto.Id)} , 
            s.data_type as {nameof(TranslatedSpecResponseDto.DataType)},
            st.name as {nameof(TranslatedSpecResponseDto.Name)}, 
            COALESCE(array_agg(sp.value) FILTER (WHERE sp.value IS NOT NULL), ARRAY[]::text[]) as {nameof(TranslatedSpecResponseDto.Options)}
        FROM
            {Schemas.Catalog}.specification as s 
        LEFT JOIN
            {Schemas.Catalog}.specification_translation as st
        ON
            s.id = st.spec_id AND st.lang_code = @lang_code
        LEFT JOIN 
            {Schemas.Catalog}.specification_option as sp
        ON
            sp.specification_id = s.id   
        {(name is not null ? "WHERE st.name ILIKE @name" : "")}
        GROUP BY
            s.id, s.data_type, st.name
        LIMIT @pageSize OFFSET @offset
        """;

        return [.. await connection.QueryAsync<TranslatedSpecResponseDto>(
                sql: query,
                param: new { name, lang_code , pageSize , offset })];
    }
    public async Task<int> Total(string? name, Language lang_code)
    {
        if (!string.IsNullOrEmpty(name))
            name += "%";
        else
            name = null;
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string query =
        $"""
        SELECT
            COUNT(st.name)
        FROM
            {Schemas.Catalog}.specification_translation as st
        WHERE
            st.lang_code = @lang_code {(string.IsNullOrWhiteSpace(name) ? "" : "AND CT.name ILIKE @name")}
        """;
        return await connection.ExecuteScalarAsync<int>(query, new { name, lang_code });
    }

    public async Task<ICollection<TranslatedSpecResponseDto>> GetByCategoryId(int categoryId, int[] Path, Language langCode)
    {
        // TODO CACHE THE PATH USING THE CATEGORY ID 
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string query =
        $@"
        SELECT
            s.id as {nameof(TranslatedSpecResponseDto.Id)} , 
            st.name as {nameof(TranslatedSpecResponseDto.Name)} , 
            s.data_type as {nameof(TranslatedSpecResponseDto.DataType)} ,
            COALESCE(array_agg(so.value) FILTER (WHERE so.value IS NOT NULL), ARRAY[]::text[]) as {nameof(TranslatedSpecResponseDto.Options)} 
        FROM
            {Schemas.Catalog}.category_spec as cs
        LEFT JOIN
            {Schemas.Catalog}.specification as s
        ON
            cs.spec_id = s.id
        LEFT JOIN
            {Schemas.Catalog}.specification_option as so
        ON
            so.specification_id = s.id
        LEFT JOIN
            {Schemas.Catalog}.specification_translation as st
        ON
            s.id = st.spec_id
        WHERE
            st.lang_code = @lang_code AND cs.category_id = ANY(@ids)
        GROUP BY
            s.id, st.name , s.data_type ;
        ";
        return [ .. await connection.QueryAsync<TranslatedSpecResponseDto>(
            sql: query,
            param: new { ids = Path, lang_code = langCode }) ];


    }


}
