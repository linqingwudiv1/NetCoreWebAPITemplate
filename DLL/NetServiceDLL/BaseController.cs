using DTOModelDLL.Common;
using DTOModelDLL.Common.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetApplictionServiceDLL
{
    /// <summary>
    /// 基础 controller
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected DTO_StoreAccount GetStoreAccount()
        {
            return this.HttpContext.Session.GetStoreAccount();
        }


        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(value);
            return base.Ok(ret_data);
        }

        public override NotFoundObjectResult NotFound([ActionResultObjectValue] object value)
        {
            DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(value, 40400,40400);
            return base.NotFound(ret_data);
        }
    }
}
