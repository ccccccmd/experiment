using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace Cacheing
{
    public class RuntimeCacheProvider : ICacheProvider
    {
        public void Clear()
        {
            var cache = HttpRuntime.Cache;
            var cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                cache.Remove(cacheEnum.Key.ToString());
            }
        }

        public void Clear(string pattern)
        {
            var cache = HttpRuntime.Cache;
            var cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                var key = cacheEnum.Key.ToString();

                if (Regex.IsMatch(key, pattern))
                {
                    cache.Remove(cacheEnum.Key.ToString());
                }

            }
        }

        public object Get(string key)
        {
            return HttpRuntime.Cache[key];
        }

        public T Get<T>(string key) where T : class
        {
            return HttpRuntime.Cache[key] as T;
        }

        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        public void Set(string key, object value, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.MaxValue, slidingExpiration);
        }

        public void Set(string key, object value, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration);
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration) where T : class
        {
            HttpRuntime.Cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration);
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration) where T : class
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.MaxValue, slidingExpiration);
        }
    }
}