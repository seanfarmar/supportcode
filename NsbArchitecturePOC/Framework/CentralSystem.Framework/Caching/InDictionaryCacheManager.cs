namespace CentralSystem.Framework.Caching
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Dictionary based cache manager.
    /// Used for simple caching of items on business component instance level.
    /// </summary>
    public sealed class InDictionaryCacheManager : ICacheManager
    {

        #region Members

        /// <summary>
        /// Concurrent dictionary
        /// </summary>
        private readonly IDictionary<string, object> m_dictionary;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public InDictionaryCacheManager() : this(new ConcurrentDictionary<string, object>())
        {
        }

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="dictionary"></param>
        public InDictionaryCacheManager(IDictionary<string, object> dictionary)
        {
            m_dictionary = dictionary;
        }

        #endregion

        #region ICacheManager Members

        /// <summary>
        /// Inserts a cache entry into the cache
        /// </summary>
        /// <param name="key"> A unique identifier for the cache entry</param>
        /// <param name="value">The object to insert</param>
        public void SetItem(string key, object value)
        {
            m_dictionary[key] = value;
        }

        /// <summary>
        /// Determines whether a cache entry exists in the cache.
        /// </summary>
        /// <param name="key">A unique identifier for the cache entry to search for.</param>
        /// <returns>true if the cache contains a cache entry whose key matches key; otherwise, false.</returns>
        public bool Contains(string key)
        {
            return m_dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Returns the total number of cache entries in the cache.
        /// </summary>
        /// <returns>The number of entries in the cache.</returns>
        public long Count()
        {
            return m_dictionary.Count;
        }

        /// <summary>
        /// Gets value from the cache
        /// </summary>
        /// <param name="key">A unique identifier for the cache value to get or set.</param>
        /// <returns>The value in the cache instance for the specified key, if the entry exists; otherwise, null.</returns>
        public object GetItem(string key)
        {
            object result = null;
            if (!m_dictionary.TryGetValue(key, out result))
            {
                result = null;
            }
            return result;
        }

        #endregion

    }
}
