using BaseDLL.Helper.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace DBAccessBaseDLL.IDGenerator
{
    /// <summary>
    /// 
    /// </summary>
    class RedisIDGenerator : IIDGenerator
    {
        /// <summary>
        ///  Redis ID生成器
        /// </summary>
        protected RedisExChangeHelper RedisIns { get; private set; }

        /// <summary>
        /// Redis 地址数组:ID 生成器
        /// </summary>
        protected IList<string> RedisAddress { get; set; }

        /// <summary>
        /// Redis 密码
        /// </summary>
        public string RedisPassword { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_RedisAddress"></param>
        /// <param name="_RedisPassword"></param>
        public RedisIDGenerator(IList<string>  _RedisAddress, string _RedisPassword)
        {
            RedisAddress = _RedisAddress;
            RedisPassword = _RedisPassword;
            this.InitRedis();
        }


        /// <summary>
        /// 初始化 Redis 用户Key ID生成器
        /// </summary>
        /// <returns></returns>
        protected bool InitRedis()
        {
            if (RedisAddress.Count <= 0)
            {
                Console.WriteLine("hello Debug Develop");
                RedisAddress.Add("127.0.0.1:10110");
            }
            RedisIns = new RedisExChangeHelper(RedisAddress, Password: RedisPassword);
            return true;
        }

        public long GetNewID<T>()
        {
            string key = "IDIncr_" + typeof(T).FullName;
            Int64 newID = RedisIns.Incr(key);
            return newID;
        }
    }
}
