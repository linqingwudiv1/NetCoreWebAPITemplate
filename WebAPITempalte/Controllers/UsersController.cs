using DTOModelDLL.API;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetApplictionServiceDLL;
using WebAPI.Model.Static;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Vue项目展示接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [EnableCors("WebAPIPolicy")]
    public class UsersController : BaseController
    {
        private Option_ConnctionString Opt_Conn { get; set; }
        private Opt_API_LTEUrl Opt_API { get; set; }

        /// <summary>
        /// 测试  
        /// </summary>
        /// <param name="Opt"></param>
        /// <param name="_Opt_API"></param>
        public UsersController(IOptions<Option_ConnctionString> Opt, IOptions<Opt_API_LTEUrl> _Opt_API)
        {
            Opt_Conn = Opt.Value;
            Opt_API  = _Opt_API.Value;
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="loginInfo">登录信息</param>
        /// <returns></returns>
        [HttpPost]
        public dynamic Login([FromBody] DTOAPI_Login loginInfo)
        {
            return "{abc123}";
        }

        /// <summary>
        /// `
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic Logout([FromBody] DTOAPI_Login loginInfo)
        {
            return "{abc123}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic Info([FromBody] DTOAPI_Login loginInfo)
        {
            return "{abc123}";
        }

    }
}
