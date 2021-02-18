using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.Account
{
    /// <summary>
    /// 
    /// </summary>
    public class RegisterAccountByPhoneCommand
    {

        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string phoneAreaCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string password { get; set; }
    }
}
