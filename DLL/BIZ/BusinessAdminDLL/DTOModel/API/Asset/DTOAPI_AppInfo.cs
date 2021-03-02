using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BusinessAdminDLL.DTOModel.API.Asset
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPI_AppInfo
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
        [DefaultValue(false)]
        public bool bLatest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(false)]
        public bool bForceUpdate { get; set; }
    }
}
