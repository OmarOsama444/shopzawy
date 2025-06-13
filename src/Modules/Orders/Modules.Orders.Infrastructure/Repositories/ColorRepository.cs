using System.Data;
using System.Data.Common;
using Dapper;
using MassTransit.SagaStateMachine;
using Microsoft.EntityFrameworkCore;
using Modules.Common.Infrastructure;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class ColorRepository : Repository<Color, OrdersDbContext>, IColorRepository
{
    public OrdersDbContext ordersDbContext { get; init; }
    public IDbConnectionFactory dbConnectionFactory { get; init; }
    public ColorRepository(OrdersDbContext ordersDbContext, IDbConnectionFactory dbConnectionFactory) : base(ordersDbContext)
    {
        this.ordersDbContext = ordersDbContext;
        this.dbConnectionFactory = dbConnectionFactory;
    }
    public async Task<Color?> GetByName(string name)
    {
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        string Query =
        $"""
        SELECT
        code as {nameof(Color.Code)},
        name as {nameof(Color.Name)}
        FROM
        {Schemas.Orders}.Color
        WHERE
        {nameof(Color.Name)} = @name
        """;
        return connection.QueryFirstOrDefault<Color>(Query, new { name });
    }

    public async Task<ICollection<ColorResponse>> Paginate(int pageSize, int pageNumber, string? name)
    {
        await using DbConnection connection = await dbConnectionFactory.CreateSqlConnection();
        int offset = (pageNumber - 1) * pageSize;
        string Query =
        $"""
        SELECT
        code as {nameof(ColorResponse.code)},
        name as {nameof(ColorResponse.name)}
        FROM
        {Schemas.Orders}.Color
        WHERE
            ( @name is NULL ) OR ( name ILIKE @name )
        LIMIT 
            @pageSize 
        OFFSET 
            @offset;
        """;
        return (await connection.QueryAsync<ColorResponse>(Query, new { pageSize, offset, name })).ToList();
    }

    public async Task<int> TotalColors(string? name)
    {
        IQueryable<Color> colors = name is null ? context.Colors : context.Colors.Where(x => x.Name.StartsWith(name));
        return await colors.CountAsync();
    }
}
