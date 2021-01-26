using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DefaultValue("")]
        public string icon { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("")]
        public string title { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("")]
        public string activeMenu { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(false)]
        public bool hidden { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(false)]
        public bool noCache { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(false)]
        public bool affix { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(false)]
        public bool alwaysShow { get; set; } = false;
    }
}
