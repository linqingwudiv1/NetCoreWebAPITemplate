using BaseDLL.DTO;
using BaseDLL.DTO.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(value);
            return base.Ok(ret_data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override JsonResult Json(object data)
        {
            DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(data);
            return base.Json(ret_data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serializerSettings"></param>
        /// <returns></returns>
        public override JsonResult Json(object data, object serializerSettings)
        {
            DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(data);
            return base.Json(ret_data, serializerSettings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override NotFoundObjectResult NotFound([ActionResultObjectValue] object value)
        {
            DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(value, 40400,40400);
            return base.NotFound(ret_data);
        }
    }
}
