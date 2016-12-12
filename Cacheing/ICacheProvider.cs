using System;

namespace Cacheing
{
    public interface ICacheProvider
    {
        /// <summary>
        ///     获取缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        ///     获取强类型缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key) where T : class;

        /// <summary>
        ///     设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        void Set(string key, object value, DateTime absoluteExpiration);

        /// <summary>
        ///     设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpiration">滑动过期</param>
        void Set(string key, object value, TimeSpan slidingExpiration);

        /// <summary>
        ///     设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpiration">滑动过期</param>
        void Set<T>(string key, T value, TimeSpan slidingExpiration) where T : class;

        /// <summary>
        ///     设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration">绝对过期时间</param>
        void Set<T>(string key, T value, DateTime absoluteExpiration) where T : class;


        /// <summary>
        ///     移除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        ///     清空缓存
        /// </summary>
        void Clear();

        /// <summary>
        ///     根据通配符查找清除缓存
        /// </summary>
        /// <param name="pattern"></param>
        void Clear(string pattern);
    }
}