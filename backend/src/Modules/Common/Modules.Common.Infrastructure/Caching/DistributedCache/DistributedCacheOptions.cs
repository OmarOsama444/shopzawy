using Microsoft.Extensions.Caching.Distributed;

namespace Modules.Common.Infrastructure.Caching.DistributedCache
{
    public class CacheOptions
    {
        public static DistributedCacheEntryOptions DefaultExporation => new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
        };

        public static DistributedCacheEntryOptions Create(TimeSpan? timeSpan)
        {
            return
                timeSpan.HasValue ?
                new() { AbsoluteExpirationRelativeToNow = timeSpan.Value } :
                DefaultExporation;
        }
    }
}
