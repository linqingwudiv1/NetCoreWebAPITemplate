using DBAccessCoreDLL.EF.Entity;
using System;
using System.Collections.Generic;

namespace BusinessCoreDLL.DTOModel.API.Routes
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
        /// 
        /// </summary>
        public string title { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string path { get; set; }

    }
}
