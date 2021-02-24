using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessCoreDLL.DTOModel.API.Users
{
    /// <summary>
    /// Request Login
    /// </summary>
    public class DTOAPIReq_Login
    {
        /// <summary>
        /// 用户名/Email/UID/Phone/passport
        /// </summary>
        [Required]
        public string username { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Required]
        public string password { get; set; }

        /// <summary>
        /// 类型 id, phone, email, passport, username
        /// </summary>
        public string type { get; set; }
    }
}
