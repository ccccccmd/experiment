using System;
using System.Net;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace Cacheing
{
    public class MemcacheCacheProvider : ICacheProvider
    {
        private readonly MemcachedClient _client = MemcachedHelper.Client;
        public void Clear()
        {
            _client.FlushAll();
        }

        public void Clear(string pattern)
        {
            
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            return _client.Get(key);
        }

        public T Get<T>(string key) where T : class
        {
            return _client.Get<T>(key);
        }

        public void Remove(string key)
        {
            _client.Remove(key);
        }

        public void Set(string key, object value, TimeSpan slidingExpiration)
        {
            _client.Store(StoreMode.Set, key, value, slidingExpiration);
        }

        public void Set(string key, object value, DateTime absoluteExpiration)
        {
            _client.Store(StoreMode.Set, key, value, absoluteExpiration);
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration) where T : class
        {
            _client.Store(StoreMode.Set, key, value, absoluteExpiration);
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration) where T : class
        {
            _client.Store(StoreMode.Set, key, value, slidingExpiration);
        }
    }
}