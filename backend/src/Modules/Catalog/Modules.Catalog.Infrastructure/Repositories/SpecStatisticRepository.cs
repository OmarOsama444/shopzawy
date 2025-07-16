using System.Data.Common;
using Common.Application;
using Common.Domain.ValueObjects;
using Common.Infrastructure;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Dtos;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities.Views;
using Modules.Catalog.Infrastructure.Data;


namespace Modules.Catalog.Infrastructure.Repositories;

public class SpecStatisticRepository
    (OrdersDbContext ordersDbContext, IDbConnectionFactory dbConnectionFactory) :
    Repository<SpecificationStatistics, OrdersDbContext>(ordersDbContext), ISpecStatisticRepository
{
    public async Task<ICollection<TranslatedSpecStatisticsDto>> GetByCategoryId(int Id, int[] Path, Language langCode)
    {
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string query =
        $@"
        SELECT DISTINCT
            ss.id as {nameof(TranslatedSpecStatisticsDto.Id)} , 
            st.name {nameof(TranslatedSpecStatisticsDto.Name)} , 
            ss.data_type as {nameof(TranslatedSpecStatisticsDto.DataType)} ,
            ss.value as {nameof(TranslatedSpecStatisticsDto.Value)} ,
            ss.total_products as {nameof(TranslatedSpecStatisticsDto.TotalProducts)} ,
            ss.created_on_utc as {nameof(TranslatedSpecStatisticsDto.CreatedOnUtc)}
        FROM
            {Schemas.Catalog}.category_spec as cs
        LEFT JOIN
            {Schemas.Catalog}.specification_statistics as ss
        ON
            cs.spec_id = ss.id
        LEFT JOIN
            {Schemas.Catalog}.specification_translation as st
        ON
            ss.id = st.spec_id
        WHERE
            st.lang_code = @langCode AND cs.category_id = ANY(@ids) AND ss.total_products != 0
        ORDER BY 
            ss.created_on_utc
        ";
        return [.. await connection.QueryAsync<TranslatedSpecStatisticsDto>(query, new { langCode, ids = Path })];
    }


    public Task<SpecificationStatistics?> GetByIdAndValueAsync(Guid id, string value, CancellationToken cancellationToken = default)
    {
        return context.SpecificationStatistics
            .FirstOrDefaultAsync(x => x.Id == id && x.Value == value, cancellationToken);
    }

}
