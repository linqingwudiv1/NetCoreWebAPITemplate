using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.Account
{
    /// <summary>
    /// 
    /// </summary>
    public class RegisterAccountByPassportCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string passport { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string password { get; set; }
    }
}
