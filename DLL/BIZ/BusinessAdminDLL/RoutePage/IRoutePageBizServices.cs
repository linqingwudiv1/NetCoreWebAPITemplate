﻿using BaseDLL.Helper;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using System;
using DBAccessCoreDLL.EFORM.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        Task<DTOAPIRes_RoutePages> GetRoutePages();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DTOAPIRes_RoutePages> GetRoutePage(Int64 Id);

        /// <summary>
        /// 
        /// </summary>
        Task<dynamic> AddRoutePage(DTOAPI_RoutePages routepage);

        /// <summary>
        /// 
        /// </summary>
        Task<dynamic> AddRoutePages(DTOAPI_RoutePages routepage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routepage"></param>
        /// <returns></returns>
        Task<dynamic> UpdateRoutePage(DTOAPI_RoutePages routepage);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<dynamic> DeleteRoutePage(long id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<IList<DTOAPI_RoutePages>> GetRoutePageTreeByRoles(IList<long> ids);
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        Task<dynamic> GetRoutePageByRoles(IList<long> roles);
    }
}
