using DBAccessBaseDLL.Accesser;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessCoreDLL.Accesser
{
    public interface IRoutePageAccesser : IAccesser<RoutePages, Int64>
    {
        /// <summary>
        /// DAO Layer
        /// </summary>
        CoreContextDIP db { get;protected set; }
    }
}
