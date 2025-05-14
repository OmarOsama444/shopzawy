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

public class SpecRepository(OrdersDbContext ordersDbContext, IDbConnectionFactory dbConnectionFactory) :
    Repository<Specification, OrdersDbContext>(ordersDbContext),
    ISpecRepository
{
    public async Task<ICollection<Specification>> GetByDataType(string dataTypeName)
    {
        return await context.Specifications.Where(s => s.DataType == dataTypeName).ToListAsync();
    }
    public async Task<ICollection<SpecResponse>> Paginate(int pageNumber, int pageSize, string? name, Language lang_code)
    {
        if (!string.IsNullOrEmpty(name))
            name = name + "%";
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string query =
        $"""
        SELECT
            s.id as id , 
            s.datatype as data_type ,
            st.name as name , 
            sp.id as option_id , 
            sp.value as value  
        FROM
            {Schemas.Orders}.specification as s 
        LEFT JOIN
            {Schemas.Orders}.specification_translation as st
        ON
            s.id = st.spec_id AND st.lang_code = @lang_code
        LEFT JOIN 
            {Schemas.Orders}.specification_option as sp
        ON
            sp.specification_id = s.id  
        WHERE
            @name IS NULL OR st.name ILike @name
        """;

        var ret = await connection.QueryAsync<SpecResponse, SpecOptionsResponse, SpecResponse>(
                sql: query,
                map: (specResponse, specOptionsResponse) =>
                {
                    if (specOptionsResponse is null)
                        specResponse.Options = new List<SpecOptionsResponse>();
                    return specResponse;
                },
                param: new { name, lang_code },
                splitOn: "option_id"
                );
        return ret
            .GroupBy(sr => sr.id)
            .Select(s =>
            {
                SpecResponse spec = s.First();
                spec.Options = s.SelectMany(x => x.Options).ToList();
                return spec;
            }).ToList();
    }
    public async Task<int> Total(string? name, Language lang_code)
    {
        if (!string.IsNullOrEmpty(name))
            name = name + "%";
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string query =
        $"""
        SELECT
            COUNT(st.name)
        FROM
            {Schemas.Orders}.specification_translation as st
        WHERE
            st.lang_code = @lang_code AND @name IS NULL OR st.name ILike @name
        """;
        return await connection.ExecuteScalarAsync<int>(query, new { name, lang_code });
    }

}