using Core.Domain.Shared.Cache;
using Core.Domain.Shared.Export;
using Microsoft.Extensions.Caching.Memory;

namespace Core.MemoryCache
{
    // chưa hoàn thiện
    public class BaseCached : ICached
    {
        private readonly IMemoryCache memoryCache;
        /// <summary>
        /// Gets the item associated with this key if present.
        /// </summary>
        /// <param name="key">An object identifying the requested entry.</param>
        /// <param name="value">The located value or null.</param>
        /// <returns>True if the key was found.</returns>
        bool ICached.TryGetValue(object key, out object? value)
        {
            return memoryCache.TryGetValue(key, out value);
        }

        /// <summary>
        /// Create or overwrite an entry in the cache.
        /// </summary>
        /// <param name="key">An object identifying the entry.</param>
        /// <returns>The newly created <see cref="ICacheEntry"/> instance.</returns>
        ICacheEntry ICached.CreateEntry(object key)
        {
            return memoryCache.CreateEntry(key);
        }

        /// <summary>
        /// Removes the object associated with the given key.
        /// </summary>
        /// <param name="key">An object identifying the entry.</param>
        void ICached.Remove(object key)
        {
            memoryCache?.Remove(key);
        }

        public T Get<T>(string key, bool removeAfterGet = false)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string Key, T data, TimeSpan? expired = null)
        {
            throw new NotImplementedException();
        }

        public void SetCache<T>(CacheRequest cache, T param)
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(2),
                Size = 1024
            };
            memoryCache.Set(cache.Key, param, cacheOptions);
        }
    }
}
