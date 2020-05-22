using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.DTOModel.API.Users
{
    /// <summary>
    /// Info
    /// </summary>
    public class DTOAPIRes_Info
    {
        /// <summary>
        /// 
        /// </summary>
        public Int64 id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string introduction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<string> roles { get; set; }
    }
}
