using System;
using System.Collections.Generic;
using System.Text;

namespace DTOModelDLL.API
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
