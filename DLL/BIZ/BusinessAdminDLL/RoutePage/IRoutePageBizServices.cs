using BaseDLL.Helper;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using System;
using RoutePage_Alias = DBAccessCoreDLL.EFORM.Entity.RoutePages;

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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        TreeItem<RoutePage_Alias> GetRoutePage(Int64 Id);

        /// <summary>
        /// 
        /// </summary>
        dynamic AddRoutePage(DTOAPI_RoutePages routepage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routepage"></param>
        /// <returns></returns>
        dynamic UpdateRoutePage(DTOAPI_RoutePages routepage);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        dynamic DeleteRoutePage(long id);
    }
}
