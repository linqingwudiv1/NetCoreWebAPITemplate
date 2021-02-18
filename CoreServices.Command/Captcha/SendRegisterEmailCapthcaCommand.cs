using BaseDLL.Helper.Captcha;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.Captcha
{
    /// <summary>
    /// 
    /// </summary>
    public class SendRegisterEmailCapthcaCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CaptchaType type { get; set; } = CaptchaType.PureNumber;

        /// <summary>
        /// 
        /// </summary>
        public int timeout { get; set; } = 900;
    }
}
