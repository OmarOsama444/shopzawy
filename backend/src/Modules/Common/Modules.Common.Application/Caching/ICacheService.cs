using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Common.Application.Caching
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken token = default ); 
        Task SetAsync<T>(
            string key,
            T value , 
            TimeSpan? expiration = null , 
            CancellationToken token = default);
        Task Remove( string key , CancellationToken token = default );
    }
}
