using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DBAccessCoreDLL.Validator
{
    /// <summary>
    /// 
    /// </summary>
    static public class AccountValidator
    {
        /// <summary>
        /// 数字与字母下划线组成的字符串
        /// </summary>
        private static Regex Rex_Passport = new Regex(@"^(?![0-9]+$)(?![a-zA-Z]+$)[a-zA-Z0-9_]{7,23}$");

        /// <summary>
        /// 必须由字母与数字组成的包含数字字母以及特殊字符组成的字符串
        /// </summary>
        private static Regex Rex_Pwd = new Regex(@"^(?![0-9]+$)(?![a-zA-Z]+$)[a-zA-Z0-9\\W_!@#$%^&*`~()-+=<>?,./;':""\[\]]{7,23}$");

        /// <summary>
        /// 
        /// </summary>
        private static Regex Rex_Username = new Regex(@"^[a-zA-Z0-9_\u4e00-\u9fa5]{2,17}$");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passport"></param>
        /// <returns></returns>
        public static bool bValidPassport(string passport) 
        {
            return Rex_Passport.IsMatch(passport);
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool bValidPassword(string pwd) 
        {
            return Rex_Pwd.IsMatch(pwd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool bValidUserName(string userName) 
        {
            return Rex_Username.IsMatch(userName);
        }
    }
}
