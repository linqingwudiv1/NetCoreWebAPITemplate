using DBAccessBaseDLL.Accesser;
using DBAccessCoreDLL.EF.Context;
using DBAccessCoreDLL.EF.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessCoreDLL.Accesser
{
    public interface IRoutePageAccesser : IAccesser<RoutePage, Int64>
    {
        /// <summary>
        /// DAO Layer
        /// </summary>
        CoreContextDIP db { get;protected set; }
    }
}
