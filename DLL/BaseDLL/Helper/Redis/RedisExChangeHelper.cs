using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace BaseDLL.Helper.Redis
{
    /// <summary>
    /// 参考
    /// </summary>
    public class RedisExChangeHelper : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private ConnectionMultiplexer RedisMgr = null;
        
        /// <summary>
        /// 
        /// </summary>
        public IDatabase Redis { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        static string RedisIPAddress = "127.0.0.1:6379";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RedisAddressList"></param>
        /// <param name="Password"></param>
        /// <param name="DbIndex"></param>
        public RedisExChangeHelper(int DbIndex = -1)
        {
            RedisMgr = ConnectionMultiplexer.Connect(RedisIPAddress);

            Redis = RedisMgr.GetDatabase(DbIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RedisAddressList"></param>
        /// <param name="Password">默认无密码</param>
        /// <param name="DbIndex"></param>
        public RedisExChangeHelper(IList<string> RedisAddressList = null, string Password = "", int DbIndex = -1)
        {
            ConfigurationOptions opt = new ConfigurationOptions
            {
                CommandMap = CommandMap.Create(new HashSet<string>
                {
                    "INFO", "CONFIG",
                    "CLUSTER","PING",
                    "ECHO", "CLIENT"
                }, available: false),
                KeepAlive = 120
            };

            if (RedisAddressList == null || RedisAddressList.Count == 0)
            {
                RedisAddressList.Add("127.0.0.1:6379");
            }

            if (!string.IsNullOrEmpty(Password))
            {
                opt.Password = Password;
            }


            foreach (var item in RedisAddressList)
            {
                opt.EndPoints.Add(item);
            }

            RedisMgr = ConnectionMultiplexer.Connect(opt);
            Redis = RedisMgr.GetDatabase(DbIndex);
        }

        /// <summary>
        /// 切换当前DB
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public bool SwitchDB(int Index)
        {
            IDatabase _redis = RedisMgr.GetDatabase(Index);
            if (_redis != null)
            {
                Redis = _redis;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int CurrentDB()
        {
            if (Redis != null)
            {
                return Redis.Database;
            }
            return -2;
        }

        /// <summary>
        /// 判断Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            Redis.KeyExists(key);
            bool ret_result = false;
            return ret_result;
        }

        /// <summary>
        /// 距离过期时间还有多少时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TimeSpan TTL(string key)
        {
            return Redis.KeyTimeToLive(key).Value;
        }

        /// <summary>
        /// 设置过期时间(秒)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeout">秒</param>
        public void Expire(string key, int timeout = -1)
        {
            TimeSpan? timespan = null;
            if (timeout > 0)
            {
                timespan = TTL(key).Add(new TimeSpan(0, 0, timeout));
            }

            Redis.KeyExpire(key, timespan);
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="timeout"></param>
        public void Expire(string key, TimeSpan timeout)
        {
            if (timeout.TotalMilliseconds >= 0)
            {
                Redis.KeyExpire(key, timeout);
            }
        }


        #region Key/Value存储

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存建</param>
        /// <param name="value">缓存值</param>
        /// <param name="timeout">过期时间，单位秒,-1：不过期</param>
        /// <returns></returns>
        public bool Set(string key, string value, int timeout = -1)
        {
            TimeSpan? timespan = null;
            if (timeout > 0)
            {
                timespan = new TimeSpan(0, 0, timeout);
            }

            return Redis.StringSet(key, value, timespan);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public Task<bool> SetAsync(string key, string value, int timeout = -1)
        {
            TimeSpan? timespan = null;
            if (timeout > 0)
            {
                timespan = new TimeSpan(0, 0, timeout);
            }

            return Redis.StringSetAsync(key, value, timespan);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存建</param>
        /// <param name="t">缓存值</param>
        /// <param name="timeout">过期时间，单位秒,-1：不过期</param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, int timeout = -1)
        {
            TimeSpan? timespan = null;
            if (timeout > 0)
            {
                timespan = new TimeSpan(0, 0, timeout);
            }

            string val_json = JsonConvert.SerializeObject(value);
            return Redis.StringSet(key, val_json, timespan);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sec"></param>
        /// <param name="value"></param>
        public void SetEX(string key, string value, int sec = -1)
        {
            SetEX<string>(key, value, sec);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="sec"></param>
        /// <param name="value"></param>
        public void SetEX<T>(string key, T value, int sec = -1)
        {
            TimeSpan? timespan = null;
            if (sec > 0)
            {
                timespan = new TimeSpan(0, 0, sec);
            }

            var str = JsonConvert.SerializeObject(value);
            Redis.StringSet(key, str, timespan);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">建</param>
        /// <returns></returns>
        public string Get(string key)
        {
            string data = Redis.StringGet(key);
            return data;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            string data = Redis.StringGet(key);
            if (data != null)
            {
                return JsonConvert.DeserializeObject<T>(data);
            }

            return default(T);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return Redis.KeyDelete(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> RemoveAsync(string key)
        {
            return Redis.KeyDeleteAsync(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(string key)
        {
            return Redis.KeyDelete(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(string key)
        {
            return Redis.KeyDeleteAsync(key);
        }

        /// <summary>
        ///自增 long型
        /// </summary>
        /// <param name="key"></param>
        /// <param name="incr"></param>
        /// <returns></returns>
        public long Incr(string key, long incr = 1)
        {
            return Redis.StringIncrement(key, incr);
        }

        /// <summary>
        ///自增 long型
        /// </summary>
        /// <param name="key"></param>
        /// <param name="incr"></param>
        /// <returns></returns>
        public Task<long> IncrAsync(string key, long inc = 1)
        {
            return Redis.StringIncrementAsync(key, inc);
        }

        #endregion

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (RedisMgr != null)
            {
                RedisMgr.Close();
            }
            if (Redis != null)
            {
                Redis = null;
            }
            GC.Collect();
        }
    }
}
