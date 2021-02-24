using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.Account
{
    /// <summary>
    /// 
    /// </summary>
    public class ChangeAccountNickNameCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string nickName { get; set; }
    }
}
