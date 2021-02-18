using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DBAccessCoreDLL.DTO.API.Users
{
    /// <summary>
    /// 
    /// </summary>
    public class DTO_GetUsers
    {
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("")]
        public string searchWord { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(true)]
        public bool bId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(true)]
        public bool bUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(true)]
        public bool bPassport { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(true)]
        public bool bEmail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(true)]
        public bool bPhone { get; set; }
    }
}
