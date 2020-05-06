using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDLL.Helper.SMS
{
    public class PhoneHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsVaild(string phone) 
        {
            string re = @"^1[3-9]\d{9}$";
            return false;
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
