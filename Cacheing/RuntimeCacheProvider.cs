using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cacheing
{
    public class RuntimeCacheProvider : ICacheProvider
    {
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Clear(string pattern)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object value, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object value, DateTime absoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value, DateTime absoluteExpiration) where T : class
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value, TimeSpan slidingExpiration) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
