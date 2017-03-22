using System;
using System.Diagnostics;

namespace RxLocal.Core.Caching
{
    /// <summary>
    /// Represents a manager that doesn't cache anything
    /// </summary>
    public partial class NullCacheManager : ICacheManager
    {
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key)
        {
            Log("Request to get {0} from cache was ignored.", key);
            return default(T);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            Log("Request to cache {0} was ignored.", key);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public bool IsSet(string key)
        {
            Log("Request to see if {0} is cached was ignored.", key);
            return false;
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
            Log("Request to remove {0} from cache was ignored.", key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
            Log("Request to remove items from cache using the pattern {0} was ignored.", pattern);
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
            Log("Request to clear cache was ignored.");
        }


        private void Log(string message, params object[] args)
        {
            Debug.Print("[{0:h:mm:ss.fff}] RxLocalNullCacheManager: {1}", DateTime.Now, string.Format(message, args));
        }
    }
}