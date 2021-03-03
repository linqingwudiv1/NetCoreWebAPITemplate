using DBAccessBaseDLL.Accesser;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.Entity.Asset;
using System;
using System.Linq;

namespace DBAccessCoreDLL.Accesser
{
    public interface IAppInfoAccesser : IAccesser<AppInfo, Int64>
    {
        /// <summary>
        /// DB Layer
        /// </summary>
        CoreContextDIP db { get; protected set; }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        IQueryable<AppInfo> GetByAppName(string appName);
    }
}
