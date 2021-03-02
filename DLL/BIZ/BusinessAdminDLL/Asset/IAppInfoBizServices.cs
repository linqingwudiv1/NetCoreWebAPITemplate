﻿using BaseDLL.DTO;
using BusinessAdminDLL.DTOModel.API.Asset;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAdminDLL.Asset
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppInfoBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<DTO_PageableModel<DTOAPI_AppInfo> > getAppInfos(DTO_PageableQueryModel<DTOAPIReq_GetAppInfos> info);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveAppInfo(long id);
    }
}
