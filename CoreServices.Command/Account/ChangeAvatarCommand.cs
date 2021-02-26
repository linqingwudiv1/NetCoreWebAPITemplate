using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.Account
{
    /// <summary>
    /// 
    /// </summary>
    public class ChangeAvatarCommand
    {
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatar {get;set;}
    }
}
