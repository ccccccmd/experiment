﻿using System;
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
    public class MemcachedHelper
    {


        private static readonly MemcachedClient Client;
        static MemcachedHelper()
        {
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();
            config.Servers.Add(new IPEndPoint(IPAddress.Parse("192.168.12.123"), 11211));
            config.Protocol = MemcachedProtocol.Binary;
            //config.Authentication.Type = typeof(PlainTextAuthenticator);
            //config.Authentication.Parameters["userName"] = "demo";
            //config.Authentication.Parameters["password"] = "demo";

            Client = new MemcachedClient(config);


            
        }

        public static bool  Store()
        {
            return Client.Store(StoreMode.Set ,);
        }
        public static bool Get()
        {
            return Client.Get();
        }



        public static bool Delete()
        {

        }
    }
}
