using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.Asset
{
    /// <summary>
    /// 
    /// </summary>
    public class AddAppInfoCommand
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
        public bool bLatest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool bEnable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool bBeta { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool bForceUpdate { get; set; }
    }
}
