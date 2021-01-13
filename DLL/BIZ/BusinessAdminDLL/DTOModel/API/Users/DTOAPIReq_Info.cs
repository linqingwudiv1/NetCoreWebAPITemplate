using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAdminDLL.DTOModel.API.Users
{
    /// <summary>
    /// Info
    /// </summary>
    public class DTOAPIReq_Info
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
