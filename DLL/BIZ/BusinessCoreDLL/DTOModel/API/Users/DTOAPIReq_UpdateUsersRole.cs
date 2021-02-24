using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.DTOModel.API.Users
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPIReq_UpdateUsersRole
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<long> users { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<long> roles { get; set; }
    }
}
