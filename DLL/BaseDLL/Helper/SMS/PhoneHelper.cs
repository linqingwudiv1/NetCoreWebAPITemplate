using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BaseDLL.Helper.SMS
{
    public class PhoneHelper
    {
        //static string cn_phone = "";
        static string cn_phone = @"^(86-1[3-9]\d{9})|(1[3-9]\d{9})$";
        static Regex rx_phone = new Regex(cn_phone);

        /// <summary>
        /// 是否为有效的电话号码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsValid(string phone) 
        {
            return !String.IsNullOrWhiteSpace(phone) && rx_phone.IsMatch(phone);
        }


        public static Tuple<string, string> Split(string phone, string default_areaCode = "86") 
        {
            
            var arr = phone.Split("-");

            if (arr.Length == 2)
            {
                return new Tuple<string, string>(arr[0], arr[1]);
            }
            else 
            {
                return new Tuple<string, string>(default_areaCode, arr[0]);
            }
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
