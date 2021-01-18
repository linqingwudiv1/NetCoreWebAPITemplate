using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAdminDLL.DTOModel.API.Routes
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPI_RoutePagesMeta
    {
        /// <summary>
        /// 
        /// </summary>
        public DTOAPI_RoutePagesMeta()
        {
            //noCache = affix = hidden = true;
        }
        /// <summary>
        /// 
        /// </summary>
        public string icon { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public string activeMenu { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public bool hidden { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool noCache { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool affix { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool alwaysShow { get; set; } = false;
    }
}
