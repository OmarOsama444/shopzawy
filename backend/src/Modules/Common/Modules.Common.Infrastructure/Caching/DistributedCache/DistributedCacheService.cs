using Microsoft.Extensions.Caching.Distributed;
using Modules.Common.Application.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Buffers;
using System.Text.Unicode;

namespace Modules.Common.Infrastructure.Caching.DistributedCache
{
    public class CacheService(IDistributedCache cache) : ICacheService
    {
        public async Task<T?> GetAsync<T>(string key, CancellationToken token = default)
        {
            byte[]? data = await cache.GetAsync(key, token);
            return data is null ? default : JsonSerializer.Deserialize<T>(data);

        }

        public async Task Remove(string key, CancellationToken token = default)
        {
            await cache.RemoveAsync(key, token);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken token = default)
        {
            byte[] serializedValue = JsonSerializer.SerializeToUtf8Bytes(value);
            await cache.SetAsync(key, serializedValue, CacheOptions.Create(expiration), token);
        }
    }
}
