using BusinessAdminDLL.Accounts;
using BusinessAdminDLL.DTOModel.API.Users;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdminService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/Users/")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class UserLoginController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IAccountLoginBizServices services;

        /// <summary>
        /// 
        /// </summary>
        public UserLoginController(IAccountLoginBizServices _services) 
        {
            this.services = _services;
        }

        #region Login

        /// <summary>
        /// 登录接口-账户密码
        /// </summary>
        /// <param name="userInfo">登录信息</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTOAPIReq_Login userInfo)
        {
            try
            {
                var dataInfo = await this.services.Login(userInfo);
                return JsonToCamelCase(dataInfo);
            }
            catch (Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000);
            }
        }

        /// <summary>
        /// 发送登录邮箱验证码
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> SendLoginVerifyCodeByEmail([FromBody] DTOAPI_EmailVerifyCode emailInfo)
        {
            try
            {
                await this.services.SendLoginVerifyCodeByEmail(emailInfo);
                return OkEx(null);
            }
            catch (Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000);
            }
        }

        /// <summary>
        /// 登录接口-邮箱验证码
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginByEmailVerifyCode([FromBody] DTOAPI_EmailVerifyCode emailInfo)
        {
            try
            {
                var loginInfo = await this.services.LoginByEmailVerifyCode(emailInfo);
                return OkEx(loginInfo);
            }
            catch (Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000);
            }
        }

        #endregion
    }
}
