using DBAccessCoreDLL.EFORM.Entity;
using System.Collections.Generic;

namespace BusinessAdminDLL.DTOModel.API.Routes
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPIRes_GetRoutePages
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<dynamic> constantRoutes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<dynamic> asyncRoutes { get; set; }
    }
}
