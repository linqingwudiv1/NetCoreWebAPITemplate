using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.Account
{
    /// <summary>
    /// 
    /// </summary>
    public class ChangePasswordCommand
    {
        public long key { get; set; }
        public string newPassword { get; set; }
    }
}
