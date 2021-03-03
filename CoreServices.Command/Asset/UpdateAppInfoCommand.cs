using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.Asset
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateAppInfoCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }

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
