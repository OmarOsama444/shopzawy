
using System.Data;
using System.Data.Common;

namespace Modules.Users.Application.Abstractions
{
    public interface IDbConnectionFactory
    {
        public Task<DbConnection> CreateSqlConnection();
    }
}

