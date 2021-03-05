using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DBAccessCoreDLL.DTOModel.API.Asset
{
    /// <summary>
    /// 
    /// </summary>
    public class DTO_AppInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string appName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string appVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(true)]
        public bool bLatest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(true)]
        public bool bEnable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(false)]
        public bool bBeta { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(true)]
        public bool bForceUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime createTime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime updateTime { get; set; }
    }
}
