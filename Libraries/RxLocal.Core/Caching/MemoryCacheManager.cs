using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace RxLocal.Core.Caching
{
    /// <summary>
    /// Represents a manager for caching between HTTP requests (long term caching)
    /// </summary>
    public partial class MemoryCacheManager : ICacheManager
    {
        /// <summary>
        /// Cache object
        /// </summary>
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }
        
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key)
        {
            Log("{0} retrieved from the cache.", key);
            return (T)Cache[key];
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time, in minutes</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
            {
                Log("{0} could not be set to NULL. Aborted.", key);
                return;
            }

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            policy.RemovedCallback += (args) =>
            {
                Log("{0} removed ({1})", args.CacheItem.Key, args.RemovedReason);
            };
            Cache.Set(new CacheItem(key, data), policy);
            Log("{0} cached the data for {1} minute(s).", key, cacheTime);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public virtual bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
            Cache.Remove(key);
            Log("{0} removed by request.", key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<string>();

            foreach (var item in Cache)
                if (regex.IsMatch(item.Key))
                    keysToRemove.Add(item.Key);

            foreach (string key in keysToRemove)
            {
                Remove(key);
                Log("{0} removed by pattern request.", key);
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
            Log("Cache cleared.");
        }


        private void Log(string message, params object[] args)
        {
            Debug.Print("[{0:h:mm:ss.fff}] MemoryCacheManager: {1}", DateTime.Now, string.Format(message, args));
        }
    }
}