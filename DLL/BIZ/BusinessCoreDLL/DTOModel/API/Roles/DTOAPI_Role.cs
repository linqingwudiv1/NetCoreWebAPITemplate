using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.DTOModel.API.Roles
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPI_Role
    {
        /// <summary>
        /// 
        /// </summary>
        public Int64 key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string children { get; set; }
    }
}
