using DBAccessCoreDLL.EFORM.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace BusinessAdminDLL.DTOModel.API.Routes
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPI_RoutePages
    {
        /// <summary>
        /// 
        /// </summary>
        [ DefaultValue(default(List<string>))]
        public IList<DTOAPI_RoutePages> children { get; set; }

        /// <summary>
        /// RoutePages ID
        /// </summary>
        public Int64 id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("RoutePages")]
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        public Int64? parentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("")]
        public string hierarchyPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("")]
        public string path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("")]
        public string redirect { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("")]
        public string component { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DTOAPI_RoutePagesMeta meta { get; set; }
    }
}
