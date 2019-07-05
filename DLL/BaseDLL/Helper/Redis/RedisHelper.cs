using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Configuration;
using System.Text;

namespace BaseDLL.Helper.Redis
{
    public class RedisHelper : IDisposable
    {
        private RedisClient Redis;

        //缓存池
        PooledRedisClientManager prcm = new PooledRedisClientManager();

        public string RedisIPAddress = "127.0.0.1:6379";
        public string RedisPassport = "";
        public string RedisPassword = "";

        //默认缓存过期时间单位秒
        public int secondsTimeOut = 20 * 60;

        public RedisHelper( string _RedisIPAddress = "127.0.0.1:6379", 
                            string _RedisPassport = "",
                            string _RedisPassword = "", 
                            bool OpenPooledRedis = false)
        { 
            RedisIPAddress = _RedisIPAddress;
            RedisPassport = _RedisPassport;
            RedisPassword = _RedisPassword;

            this.Redis = new RedisClient(RedisIPAddress);

            if (OpenPooledRedis)
            {
                prcm = CreateManager(new string[] { RedisIPAddress }, new string[] { RedisIPAddress });
                Redis = prcm.GetClient() as RedisClient;
            }
        }

        /// <summary>
        /// 缓冲池
        /// </summary>
        /// <param name="readWriteHosts"></param>
        /// <param name="readOnlyHosts"></param>
        /// <returns></returns>
        public static PooledRedisClientManager CreateManager(string[] readWriteHosts, string[] readOnlyHosts)
        {
            return new PooledRedisClientManager(readWriteHosts, readOnlyHosts,
                new RedisClientManagerConfig
                {
                    MaxWritePoolSize = readWriteHosts.Length * 5,
                    MaxReadPoolSize = readOnlyHosts.Length * 5,
                    AutoStart = true,
                });
        }

        /// <summary>
        /// 距离过期时间还有多少秒
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long TTL(string key)
        {
            return Redis.Ttl(key);
        }
        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeout"></param>
        public void Expire(string key, int timeout = 0)
        {
            if (timeout >= 0)
            {
                if (timeout > 0)
                {
                    secondsTimeOut = timeout;
                }
                Redis.Expire(key, secondsTimeOut);
            }
        }

        #region Key/Value存储

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="value">缓存值</param>
        /// <param name="timeout">过期时间，单位秒,-1：不过期，0：默认过期时间</param>
        /// <returns></returns>
        public bool Set(string key, string value, int timeout = 0)
        {
            Redis.Set(key, value);
            if (timeout >= 0)
            {
                if (timeout > 0)
                {
                    secondsTimeOut = timeout;
                }
                Redis.Expire(key, secondsTimeOut);
            }
            return true;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存建</param>
        /// <param name="t">缓存值</param>
        /// <param name="timeout">过期时间，单位秒,-1：不过期，0：默认过期时间</param>
        /// <returns></returns>
        public bool Set<T>(string key, T t, int timeout = 0)
        {
            Redis.Set<T>(key, t);
            if (timeout >= 0)
            {
                if (timeout > 0)
                {
                    secondsTimeOut = timeout;
                }
                Redis.Expire(key, secondsTimeOut);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sec"></param>
        /// <param name="value"></param>
        public void SetEX(string key, int sec, string value)
        {
            SetEX<string>(key, sec, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="sec"></param>
        /// <param name="value"></param>
        public void SetEX<T>(string key,int sec, T value)
        {
            var str = JsonConvert.SerializeObject(value);
            Redis.SetEx(key, sec, Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">建</param>
        /// <returns></returns>
        public string Get(string key)
        {
            var data = Redis.Get(key);
            if (data != null)
            {
                return Encoding.UTF8.GetString(data);
            }
            return String.Empty;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return Redis.Get<T>(key);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return Redis.Remove(key);
        }
        #endregion

        //释放资源
        public void Dispose()
        {
            if (Redis != null)
            {
                Redis.Dispose();
                Redis = null;
            }
            GC.Collect();
        }
    }
}
