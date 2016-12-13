using System;
using Cacheing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class MemcacheTest
    {
        [TestMethod]
        public void TestMethod()
        {

            ICacheProvider p = new MemcacheCacheProvider();
            p.Set("hello", "world", new TimeSpan(2));
            var k = p.Get("hello");
            var s = p.Get<string>("hello");
            p.Remove("hello");
            var kk = p.Get("hello");
        }
    }
}
