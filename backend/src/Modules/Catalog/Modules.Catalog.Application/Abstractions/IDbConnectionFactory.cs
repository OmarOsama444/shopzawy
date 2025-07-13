using System.Data.Common;

namespace Modules.Catalog.Application.Abstractions;

public interface IDbConnectionFactory
{
    public Task<DbConnection> CreateSqlConnection();
}
