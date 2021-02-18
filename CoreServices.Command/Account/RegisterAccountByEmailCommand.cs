using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AdminServices.Command.Account
{
    /// <summary>
    /// 
    /// </summary>
    public class RegisterAccountByEmailCommand
    {

        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string password { get; set; }
    }
}
