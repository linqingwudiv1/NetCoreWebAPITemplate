using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BusinessAdminDLL.DTOModel.API.Roles
{
    /// <summary>
    /// Add Or Update Role
    /// </summary>
    public class DTOAPIReq_Role
    {
        /// <summary>
        /// 
        /// </summary>
        public long key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("test_user")]
        public string name { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string displayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("test description ")]
        public string description { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public IList<long> routes { get; set; }
    }
}