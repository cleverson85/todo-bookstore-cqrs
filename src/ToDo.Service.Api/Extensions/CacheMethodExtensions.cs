using Microsoft.Extensions.Caching.Distributed;
using System;

namespace ToDo.Service.Api.Extensions
{
    public static class CacheMethodExtensions
    {
        public static DistributedCacheEntryOptions SetExpirationMinutes(this DistributedCacheEntryOptions distributedCache, int minutes)
        {
            distributedCache.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes);
            return distributedCache;
        }
    }
}
