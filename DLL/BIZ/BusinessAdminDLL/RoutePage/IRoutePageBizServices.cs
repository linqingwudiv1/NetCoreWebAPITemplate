using BaseDLL.Helper;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessCoreDLL.EF.Context;
using DBAccessCoreDLL.EF.Entity;

using RoutePage_Alias = DBAccessCoreDLL.EF.Entity.RoutePage;

namespace BusinessAdminDLL.RoutePage
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRoutePageBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TreeItem<RoutePage_Alias>[] GetRoutePages();

    }
}
