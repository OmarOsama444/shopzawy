using System.Data.Common;
using Common.Application;
using Common.Domain.ValueObjects;
using Common.Infrastructure;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Repositories;

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
            name = name + "%";
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string query =
        $"""
        SELECT
            s.id as {nameof(TranslatedSpecResponseDto.Id)} , 
            s.data_type as {nameof(TranslatedSpecResponseDto.DataType)},
            st.name as name as {nameof(TranslatedSpecResponseDto.Name)}, 
            sp.value as value as {nameof(SpecOptionsResponse.Value)}
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

        return (await connection.QueryAsync<TranslatedSpecResponseDto, SpecOptionsResponse, TranslatedSpecResponseDto>(
                sql: query,
                map: (specResponse, specOptionsResponse) =>
                {
                    if (specOptionsResponse is null)
                        specResponse.Options = [];
                    return specResponse;
                },
                param: new { name, lang_code },
                splitOn: "option_id"
                ))
                .GroupBy(sr => sr.Id)
                .Select(s =>
                {
                    TranslatedSpecResponseDto spec = s.First();
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

    public async Task<ICollection<TranslatedSpecResponseDto>> GetByCategoryId(Language langCode, params Guid[] categoryId)
    {
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string query =
        $@"
        SELECT DISTINCT
            s.id as {nameof(TranslatedSpecResponseDto.Id)} , 
            st.name {nameof(TranslatedSpecResponseDto.Name)} , 
            s.data_type as {nameof(TranslatedSpecResponseDto.DataType)} ,
            so.value as {nameof(SpecOptionsResponse.Value)} 
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
            st.lang_code = @lang_code AND cs.category_id = ANY(@ids);
        ";
        var specs = (await connection.QueryAsync<TranslatedSpecResponseDto, SpecOptionsResponse, TranslatedSpecResponseDto>(
            sql: query,
            map: (spec, option) =>
            {
                if (spec.Options is null || spec.Options.Count == 0)
                    spec.Options = [];
                spec.Options.Add(option.Value);
                return spec;
            },
            splitOn: "optionId",
            param: new { ids = categoryId, lang_code = langCode }))
            .GroupBy(s => s.Id)
            .Select(g =>
            {
                var spec = g.First();
                spec.Options = g.SelectMany(s => s.Options).ToList();
                return spec;
            });
        return specs.ToList();
    }
}
