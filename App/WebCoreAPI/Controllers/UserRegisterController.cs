using BusinessCoreDLL.Accounts;
using BusinessCoreDLL.DTOModel.API.Users;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreService.Controllers
{
    /// <summary>
    /// 用户注册
    /// </summary>
    [Route("api/Users")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class UserRegisterController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        readonly IAccountRegisterBizServices services;
        
        /// <summary>
        /// 
        /// </summary>
        public UserRegisterController(IAccountRegisterBizServices _services) 
        {
            services = _services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RegisterInfo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterByPassport([FromBody] DTOAPI_RegisterByPassport registerInfo)
        {
            try
            {
                var data = await this.services.RegisterPassport(registerInfo).ConfigureAwait(false);
                return OkEx(data);
            }
            catch (Exception ex) 
            {
                return this.JsonToCamelCase(ex.Message, 50000, 50000, ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> IsValidEmailCodeByRegister([FromBody] DTOAPI_EmailVerifyCode emailInfo) 
        {
            try
            {
                bool bSuccess = await this.services.IsValidEmailCodeByRegister(emailInfo).ConfigureAwait(false);
                return OkEx(new { success = bSuccess });
            }
            catch (Exception ex) 
            {
                return this.JsonToCamelCase(ex.Message, 50000, 50000, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> SendRegisterVerifyCodeByEmail([FromBody] DTOAPI_EmailVerifyCode emailInfo)
        {
            try
            {
                await this.services.SendRegisterVerifyCodeByEmail(emailInfo).ConfigureAwait(false);
                return OkEx(new { success = true});
            }
            catch (Exception ex) 
            {
                return this.JsonToCamelCase(ex.Message, 50000, 50000, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterByEmailVerifyCode([FromBody] DTOAPI_RegisterByEmailVerifyCode registerInfo) 
        {

            try
            {
                var data = await this.services.RegisterByEmailVerifyCode(registerInfo).ConfigureAwait(false);
                return OkEx(data);
            }
            catch (Exception ex)
            {
                return this.JsonToCamelCase(ex.Message, 50000, 50000, ex.Message);
            }
        }

    }
}
