using DBAccessDLL.EF.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOModelDLL.API.Routes
{
    public class DTOAPIRes_GetRoutes
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
