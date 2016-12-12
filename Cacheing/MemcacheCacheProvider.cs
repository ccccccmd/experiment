using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace Cacheing
{
    public class MemcacheCacheProvider : ICacheProvider
    {

        private static readonly MemcachedClient Client;
        static MemcacheCacheProvider()
        {
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();
            config.Servers.Add(new IPEndPoint(IPAddress.Parse("192.168.12.123"), 11211));
            config.Protocol = MemcachedProtocol.Binary;
            //config.Authentication.Type = typeof(PlainTextAuthenticator);
            //config.Authentication.Parameters["userName"] = "demo";
            //config.Authentication.Parameters["password"] = "demo";

            Client = new MemcachedClient(config);



        }


        public void Clear()
        {

            Client.FlushAll();
        }

        public void Clear(string pattern)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            return Client.Get(key);
        }

        public T Get<T>(string key) where T : class
        {
            return Client.Get<T>(key);
        }

        public void Remove(string key)
        {
            Client.Remove(key);
        }

        public void Set(string key, object value, TimeSpan slidingExpiration)
        {
            Client.Store(StoreMode.Set, key, value, slidingExpiration);
        }

        public void Set(string key, object value, DateTime absoluteExpiration)
        {
            Client.Store(StoreMode.Set, key, value, absoluteExpiration);
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration) where T : class
        {

            Client.Store(StoreMode.Set, key, value, absoluteExpiration);
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration) where T : class
        {
            Client.Store(StoreMode.Set, key, value, slidingExpiration);
        }
    }
}
