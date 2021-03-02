using DBAccessBaseDLL.Accesser;
using DBAccessCoreDLL.Entity.Asset;
using System;
using System.Linq;

namespace DBAccessCoreDLL.Accesser
{
    public interface IAppInfoAccesser : IAccesser<AppInfo, Int64>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        IQueryable<AppInfo> GetByAppName(string appName);
    }
}
