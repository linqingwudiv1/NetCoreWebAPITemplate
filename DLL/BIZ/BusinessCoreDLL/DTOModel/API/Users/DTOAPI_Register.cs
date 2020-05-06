using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.DTOModel.API.Users
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPI_Register
    {
        /// <summary>
        /// 
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// 帐号
        /// </summary>
        public string Passport { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }
    }
}
