using System;
using System.Diagnostics;

namespace RxLocal.Core.Caching
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// Variable (lock) to support thread-safe
        /// </summary>
        private static readonly object _syncObject = new object();

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="cacheTime">Cache time, in minutes (0 - do not cache)</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire) 
        {
            if (cacheManager.IsSet(key))
            {
                return cacheManager.Get<T>(key);
            }

            if (cacheTime == 0)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var result = acquire();
                stopwatch.Stop();
                Debug.Print("[{0:h:mm:ss.fff}] CacheManager: {1} not found, but caching was not requested. Acquired data in {2}ms and returned the results.", DateTime.Now, key, stopwatch.ElapsedMilliseconds);

                return result;
            }

            lock (_syncObject)
            {
                if (cacheManager.IsSet(key))
                {
                    return cacheManager.Get<T>(key);
                }

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var result = acquire();
                stopwatch.Stop();
                Debug.Print("[{0:h:mm:ss.fff}] CacheManager: {1} not found. Acquired data in {2}ms.", DateTime.Now, key, stopwatch.ElapsedMilliseconds, cacheTime);

                cacheManager.Set(key, result, cacheTime);

                return result;   
            }
        }
    }
}
