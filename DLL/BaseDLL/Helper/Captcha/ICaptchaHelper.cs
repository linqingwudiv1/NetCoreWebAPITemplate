using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDLL.Helper.Captcha
{
    /// <summary>
    /// 
    /// </summary>
    public enum CaptchaType 
    {
        /// <summary>
        /// 纯数字
        /// </summary>
        PureNumber,
        /// <summary>
        /// 字母与数字组合
        /// </summary>
        Character
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ICaptchaHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="len"></param>
        /// <param name="timeout">seconds : default 900s</param>
        /// <returns></returns>
        string NewCaptcha(string key, CaptchaType type = CaptchaType.PureNumber, int len = 6, int timeout = 900);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetCaptcha(string key);
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Captcha"></param>
        /// <returns></returns>
        bool IsValidCaptcha(string key, string Captcha);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool DeleteCaptcha(string key);
    }
}
