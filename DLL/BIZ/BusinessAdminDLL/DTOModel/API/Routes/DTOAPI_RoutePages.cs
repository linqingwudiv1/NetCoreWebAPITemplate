using DBAccessCoreDLL.EF.Entity;
using System;
using System.Collections.Generic;

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
        public IList<DTOAPI_RoutePages> children { get; set; }

        /// <summary>
        /// RoutePages ID
        /// </summary>
        public Int64 id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int64? parentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string redirect { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public string component { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DTOAPI_RoutePagesMeta meta { get; set; }
    }
}
