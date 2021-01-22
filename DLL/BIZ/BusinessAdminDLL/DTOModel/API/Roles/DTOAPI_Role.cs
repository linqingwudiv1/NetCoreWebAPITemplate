using AutoMapper;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessCoreDLL.EFORM.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAdminDLL.DTOModel.API.Roles
{
    /// <summary>
    /// 
    /// </summary>
    public class DTOAPI_Role
    {
        /// <summary>
        /// 
        /// </summary>
        public Int64 key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<DTOAPI_RoutePages> routes { get; set; }
    }

}
