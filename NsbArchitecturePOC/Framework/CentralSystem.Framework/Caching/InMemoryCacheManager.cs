namespace CentralSystem.Framework.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic Cache Manger which wraps the .NET MemoryCache object
    /// </summary>
    public sealed class InMemoryCacheManager : ICacheManager
    {
        #region Properties
        /// <summary>
        /// Gets a reference to the default System.Runtime.Caching.MemoryCache instance.
        /// </summary>
        private MemoryCache GetDefaultMemoryCache
        {
            get
            {
                return MemoryCache.Default;
            }
        } 
        #endregion

        #region ICacheManager interface
        /// <summary>
        /// Inserts a cache entry into the cache,
        /// </summary>
        /// <param name="key"> A unique identifier for the cache entry</param>
        /// <param name="value">The object to insert</param>
        public void SetItem(string key, object value)
        {            
            GetDefaultMemoryCache.Set(key, value, GetInfiniteExpirationPolicyObject());
        }

        /// <summary>
        /// Determines whether a cache entry exists in the cache.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to search for.</param>
        /// <returns>true if the cache contains a cache entry whose key matches key; otherwise, false.</returns>
        public bool Contains(string key)
        {
            return GetDefaultMemoryCache.Contains(key);
        }

        /// <summary>
        /// Returns the total number of cache entries in the cache.
        /// </summary>
        /// <returns>The number of entries in the cache.</returns>
        public long Count()
        {
            return GetDefaultMemoryCache.GetCount();
        }      

        /// <summary>
        /// Gets value from the cache
        /// </summary>
        /// <param name="key">A unique identifier for the cache value to get</param>
        /// <returns>The value in the cache instance for the specified key, if the entry exists; otherwise, null.</returns>
        public object GetItem(string key)
        {
            return GetDefaultMemoryCache[key];
        } 
        #endregion    

        #region Private methods
        /// <summary>
        /// Returns caching policy object with infinite expiration defined
        /// </summary>
        /// <returns>cache item policy</returns>
        private CacheItemPolicy GetInfiniteExpirationPolicyObject()
        {
            return new CacheItemPolicy { AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration };
        } 
        #endregion

       
    }
}
