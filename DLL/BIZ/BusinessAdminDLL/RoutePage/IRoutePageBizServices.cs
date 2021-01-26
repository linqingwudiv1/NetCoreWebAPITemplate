using BaseDLL.Helper;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using System;
using DBAccessCoreDLL.EFORM.Entity;
using System.Threading.Tasks;

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
        TreeItem<RoutePages>[] GetRoutePages();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        TreeItem<RoutePages> GetRoutePage(Int64 Id);

        /// <summary>
        /// 
        /// </summary>
        Task<int> AddRoutePage(DTOAPI_RoutePages routepage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routepage"></param>
        /// <returns></returns>
        Task<int> UpdateRoutePage(DTOAPI_RoutePages routepage);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteRoutePage(long id);
    }
}
