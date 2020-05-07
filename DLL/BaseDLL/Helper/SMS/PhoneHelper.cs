using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BaseDLL.Helper.SMS
{
    public class PhoneHelper
    {
        static string cn_phone = @"^1[3-9]\d{9}$";
        static Regex rx_phone = new Regex(cn_phone);
        
        /// <summary>
        /// 是否为有效的电话号码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsValid(string phone) 
        {
            return rx_phone.IsMatch(phone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static bool SendSMS(string Content) 
        {
            return true;
        }
    }
}
