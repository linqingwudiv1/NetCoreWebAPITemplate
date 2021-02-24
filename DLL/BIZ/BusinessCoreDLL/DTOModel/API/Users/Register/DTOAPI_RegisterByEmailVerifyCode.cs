using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.DTOModel.API.Users
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPI_RegisterByEmailVerifyCode
    {
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pwd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string verifyCode { get; set; }
    }
}
