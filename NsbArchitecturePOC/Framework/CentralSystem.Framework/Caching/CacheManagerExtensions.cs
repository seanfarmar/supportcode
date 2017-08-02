namespace CentralSystem.Framework.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Extensions methods according to ICacheManager
    /// </summary>
    public static class CacheManagerExtensions
    {

        #region Public Methods

        /// <summary>
        /// Load / create item and inserts a into the cache
        /// </summary>
        /// <typeparam name="TItem">Item type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="itemLoadFunction">Item loader/creator</param>
        /// <returns>Item</returns>
        public static TItem LoadAndSetItem<TItem>(this ICacheManager cacheManager, Func<TItem> itemLoadFunction)
        {
            return cacheManager.LoadAndSetItem(typeof(TItem).FullName, itemLoadFunction);
        }

        /// <summary>
        /// Load / create item and inserts a into the cache
        /// </summary>
        /// <typeparam name="TItem">Item type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key"> A unique identifier for the cache entry</param>
        /// <param name="itemLoadFunction">Item loader/creator</param>
        /// <returns>Item</returns>
        public static TItem LoadAndSetItem<TItem>(this ICacheManager cacheManager, string key, Func<TItem> itemLoadFunction)
        {
            TItem result = itemLoadFunction();
            cacheManager.SetItem(key, result);
            return result;
        }

        /// <summary>
        /// Check in the cache item.
        /// If not exists - load / create item and inserts a into the cache
        /// </summary>
        /// <typeparam name="TItem">Item type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="itemLoadFunction">Item loader/creator</param>
        /// <returns>Item</returns>
        public static TItem GetItemAndLoadIfMissing<TItem>(this ICacheManager cacheManager, Func<TItem> itemLoadFunction)
        {
            return cacheManager.GetItemAndLoadIfMissing(typeof(TItem).FullName, itemLoadFunction);
        }

        /// <summary>
        /// Check in the cache item.
        /// If not exists - load / create item and inserts a into the cache
        /// </summary>
        /// <typeparam name="TItem">Item type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key"> A unique identifier for the cache entry</param>
        /// <param name="itemLoadFunction">Item loader/creator</param>
        /// <returns>Item</returns>
        public static TItem GetItemAndLoadIfMissing<TItem>(this ICacheManager cacheManager, string key, Func<TItem> itemLoadFunction)
        {
            TItem result = (TItem)cacheManager.GetItem(key);

            if (result == null)
            {
                result = cacheManager.LoadAndSetItem(key, itemLoadFunction);
            }

            return result;
        }

        #endregion

    }
}
