using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAdminDLL.DTOModel.API.Users.ForgotPwd
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPI_ForgotPwdByEmailCaptcha
    {
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
    
        /// <summary>
        /// 
        /// </summary>
        public string newpwd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string verifyCode { get; set; }
    }
}
