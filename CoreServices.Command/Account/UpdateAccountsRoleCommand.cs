using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.Account
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateAccountsRoleCommand
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
