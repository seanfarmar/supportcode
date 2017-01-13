namespace CentralSystem.Framework.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines methods to use a cache
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Inserts a cache entry into the cache
        /// </summary>
        /// <param name="key"> A unique identifier for the cache entry</param>
        /// <param name="value">The object to insert</param>
        void SetItem(string key, object value);
        
        /// <summary>
        /// Determines whether a cache entry exists in the cache.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to search for.</param>
        /// <returns>true if the cache contains a cache entry whose key matches key; otherwise, false.</returns>
        bool Contains(string key);

        /// <summary>
        /// Returns the total number of cache entries in the cache.
        /// </summary>
        /// <returns>The number of entries in the cache.</returns>
        long Count();
        
        /// <summary>
        /// Gets value from the cache
        /// </summary>
        /// <param name="key">A unique identifier for the cache value to get or set.</param>
        /// <returns>The value in the cache instance for the specified key, if the entry exists; otherwise, null.</returns>
        object GetItem(string key);
    }
}
