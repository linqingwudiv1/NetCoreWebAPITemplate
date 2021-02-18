using BaseDLL.Helper.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDLL.Helper.Captcha
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisCaptchaHelper : ICaptchaHelper
    {

        static readonly string character = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static readonly string number    = "01234556789";

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
        public RedisCaptchaHelper(IList<string> _RedisAddress,string _RedisPassword) 
        {
            this.RedisAddress = _RedisAddress;
            this.RedisPassword = _RedisPassword;

            this.InitRedis();
        }

        private void InitRedis() 
        {
            if (RedisAddress.Count <= 0)
            {
                Console.WriteLine("hello Debug Develop");
                RedisAddress.Add("127.0.0.1:6379");
            }

            RedisIns = new RedisExChangeHelper(RedisAddress, Password: RedisPassword);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="len"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public string NewCaptcha(string key, CaptchaType type = CaptchaType.PureNumber, int len = 6, int timeout = 900)
        {
            Random rand = new Random();
            string s;
            StringBuilder ret_str = new StringBuilder();
            switch (type) 
            {
                case CaptchaType.Character: 
                    {
                        s = character + number;
                        break;
                    }
                case CaptchaType.PureNumber:
                    {
                        s = number;
                        break;
                    }
                default: 
                    {
                        s = character + number;
                        break; 
                    }
            }

            for (int i = 0; i < len; i++) 
            {
                int idx = rand.Next(0, s.Length - 1);
                ret_str.Append(s[idx]);
            }

            this.RedisIns.SetAsync(key, ret_str.ToString(), timeout);

            return ret_str.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Captcha"></param>
        /// <returns></returns>
        public bool IsValidCaptcha(string key, string Captcha)
        {
            string captcha = this.GetCaptcha(key);
            return !String.IsNullOrEmpty(captcha) && Captcha.ToUpper() == captcha;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetCaptcha(string key)
        {
            string captcha = this.RedisIns.Get<string>(key) ?? "";
            return captcha;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteCaptcha(string key)
        {
            this.RedisIns.RemoveAsync(key);
            return true;
        }
    }
}
