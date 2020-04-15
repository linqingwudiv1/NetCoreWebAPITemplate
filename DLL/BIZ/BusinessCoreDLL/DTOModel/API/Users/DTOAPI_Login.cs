using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.DTOModel.API.Users
{
    /// <summary>
    /// User/Login
    /// </summary>
    public class DTOAPI_Login
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string password { get; set; }
    }
}
