using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAdminDLL.DTOModel.API.Users
{
    /// <summary>
    /// Request Login
    /// </summary>
    public class DTOAPIReq_Login
    {
        /// <summary>
        /// 用户名/Email/UID/Phone/passport
        /// </summary>
        public string passport { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string password { get; set; }
    }
}
