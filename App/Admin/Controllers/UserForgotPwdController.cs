using BusinessAdminDLL.Accounts;
using BusinessAdminDLL.DTOModel.API.Users;
using BusinessAdminDLL.DTOModel.API.Users.ForgotPwd;
using Microsoft.AspNetCore.Authorization;
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
    public class UserForgotPwdController :BaseController
    {
        /// <summary>
        /// 用户接口
        /// </summary>
        private readonly IAccountFotgotPwdBizServices services;


        /// <summary>
        /// 
        /// </summary>
        public UserForgotPwdController(IAccountFotgotPwdBizServices _Services)
        {
            services = _Services;
        }

        #region Forgot Password

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> SendForgotPwdVerifyCodeByEmail([FromBody] DTOAPI_EmailVerifyCode emailInfo)
        {
            try
            {
                await this.services.SendForgotPwdVerifyCodeByEmail(emailInfo);
                return OkEx("");
            }
            catch (Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pwdInfo"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> ForgotPwdCodeByEmail([FromBody] DTOAPI_ForgotPwdByEmailCaptcha pwdInfo)
        {
            try
            {
                await this.services.ForgotPwdCodeByEmail(pwdInfo);
                return OkEx(new 
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000, _desc: ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pwdInfo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> IsValidEmailCodeByForgotPwd([FromBody] DTOAPI_ForgotPwdByEmailCaptcha pwdInfo) 
        {
            try
            {
                bool bSuccess = await this.services.IsValidEmailCodeByForgotPwd(pwdInfo);
                return OkEx(new {success = bSuccess });
            }
            catch (Exception ex) 
            {
                return JsonToCamelCase(ex.Message, 50000, 50000, _desc: ex.Message);
            }
        }

        #endregion

    }
}
