using System.Data.Common;

namespace Modules.Orders.Application.Abstractions;

public interface IDbConnectionFactory
{
    public Task<DbConnection> CreateSqlConnection();
}
