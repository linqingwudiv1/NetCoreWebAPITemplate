//using BaseDLL.DTO;
//using BaseDLL.DTO.Store;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Infrastructure;

//namespace NetApplictionServiceDLL
//{
//    /// <summary>
//    /// 基础 controller
//    /// </summary>
//    public class BaseController : Controller
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        protected DTO_StoreAccount GetStoreAccount()
//        {
//            return this.HttpContext.Session.GetStoreAccount();
//        }

//        /// <summary>
//        /// 二次封装 Ok函数
//        /// </summary>
//        /// <param name="value"></param>
//        /// <returns></returns>
//        public override OkObjectResult Ok([ActionResultObjectValue] object value)
//        {
//            DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(value);
//            return base.Ok(ret_data);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="data"></param>
//        /// <returns></returns>
//        public override JsonResult Json(object data)
//        {
//            DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(data);
//            return base.Json(ret_data);
//        }

//        /// <summary>
//        /// 二次封装 Ok函数
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="serializerSettings"></param>
//        /// <returns></returns>
//        public override JsonResult Json(object data, object serializerSettings)
//        {
//            DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(data);
//            return base.Json(ret_data, serializerSettings);
//        }

//        /// <summary>
//        /// 二次封装 Ok函数
//        /// </summary>
//        /// <param name="value"></param>
//        /// <returns></returns>
//        public override NotFoundObjectResult NotFound([ActionResultObjectValue] object value)
//        {
//            DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(value, 40400,40400);
//            return base.NotFound(ret_data);
//        }
//    }
//}















using BaseDLL.DTO;
using BaseDLL.DTO.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
        /// 二次封装 Ok函数
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
            //DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(data);
            return Json(data);
        }

        /// <summary>
        /// 二次封装 JSON函数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serializerSettings"></param>
        /// <returns></returns>
        public override JsonResult Json(object data, object serializerSettings)
        {
            //DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(data);
            return base.Json(data, serializerSettings);
        }
        /// <summary>
        /// 驼峰化,JSON属性名
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JsonResult JsonToCamelCase(object data)
        {
            var setting = new JsonSerializerSettings();
            setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return Json(data, setting);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected JsonResult Json(object data, object serializerSettings = null, int _code = 20000, int _state = 20000, string _desc = "") 
        {
            DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(data, _code, _state, _desc);
            if (serializerSettings == null)
            {
                return base.Json(ret_data);
            }
            else 
            {
                return base.Json(ret_data, serializerSettings);
            }
        }


        /// <summary>
        /// 二次封装 Ok函数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override NotFoundObjectResult NotFound([ActionResultObjectValue] object value)
        {
            DTO_ReturnModel<object> ret_data = new DTO_ReturnModel<object>(value, 40400, 40400);
            return base.NotFound(ret_data);
        }
    }
}
