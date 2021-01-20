using DBAccessCoreDLL.EFORM.Entity;
using System.Collections.Generic;

namespace BusinessCoreDLL.DTOModel.API.Routes
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPIRes_GetRoutePages
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<RoutePages> constantRoutes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<RoutePages> asyncRoutes { get; set; }
    }
}
