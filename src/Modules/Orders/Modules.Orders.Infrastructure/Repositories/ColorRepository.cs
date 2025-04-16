using System.Data;
using System.Data.Common;
using Dapper;
using MassTransit.SagaStateMachine;
using Modules.Common.Infrastructure;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Infrastructure.Data;
using Modules.Users.Infrastructure.Repositories;

namespace Modules.Orders.Infrastructure.Repositories;

public class ColorRepository(OrdersDbContext ordersDbContext, IDbConnectionFactory dbConnectionFactory) : Repository<Color, OrdersDbContext>(ordersDbContext), IColorRepository
{
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
}
