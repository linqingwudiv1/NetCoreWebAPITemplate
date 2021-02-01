using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAdminDLL.DTOModel.API.Users
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPIRes_Login
    {
        /// <summary>
        /// 
        /// </summary>
        public string accessToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string refreshToken { get; set; }


        /// <summary>
        /// accessToken过期时间/分钟
        /// </summary>
        public int expires { get; set; }

        /// <summary>
        /// refreshToken过期时间/分钟
        /// </summary>
        public int refreshExpires { get; set; }
        /// <summary>
        /// -1:未知错误 0:用户被禁用 1:登录成功, 2:密码错误, 3:用户不存在 
        /// </summary>
        public int state { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }
}
