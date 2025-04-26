using Microsoft.Extensions.Caching.Memory;
using NasaDataset.Application.Common.Interfaces;

namespace NasaDataset.Application.Common.Services
{
    public class CacheService : ICacheService
    {

        private readonly IMemoryCache _cache;

        private static readonly string[] MeteoriteCacheKeys =
        {
            "MeteoriteFilterOptions",
            "GroupedMeteoritesCache",
            "MeteoriteListCache"
        };

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void ClearMeteoriteCache()
        {
            foreach (var key in MeteoriteCacheKeys)
            {
                _cache.Remove(key);
            }
        }

        public T? Get<T>(string key)
        {
            return _cache.TryGetValue(key, out var value) ? (T?)value : default;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Set<T>(string key, T value, TimeSpan duration)
        {
            _cache.Set(key, value, duration);
        }
    }
}
