using Core.Domain.Shared.Cache;
using Core.Domain.Shared.Export;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Core.MemoryCache
{
    public interface ICached
    {
        /// <summary>
        /// Gets the item associated with this key if present.
        /// </summary>
        /// <param name="key">An object identifying the requested entry.</param>
        /// <param name="value">The located value or null.</param>
        /// <returns>True if the key was found.</returns>
        bool TryGetValue(object key, out object? value);

        /// <summary>
        /// Create or overwrite an entry in the cache.
        /// </summary>
        /// <param name="key">An object identifying the entry.</param>
        /// <returns>The newly created <see cref="ICacheEntry"/> instance.</returns>
        ICacheEntry CreateEntry(object key);

        /// <summary>
        /// Removes the object associated with the given key.
        /// </summary>
        /// <param name="key">An object identifying the entry.</param>
        void Remove(object key);
        /// <summary>
        /// Hàm này dùng để lưu cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="data"></param>
        /// <param name="expired"></param>
        void Set<T>(string Key, T data, TimeSpan? expired = null);
        /// <summary>
        /// Hàm này dùng để lấy giá trị trong cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="removeAfterGet"></param>
        /// <returns></returns>
        T Get<T>(string key, bool removeAfterGet = false);
        /// <summary>
        /// Hàm này để xóa dữ liệu trong cache
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        public void SetCache<T>(CacheRequest cache, T param);
    }
}
