using DBAccessCoreDLL.EF.Entity;
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
        public IList<RoutePage> constantRoutes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<RoutePage> asyncRoutes { get; set; }
    }
}
