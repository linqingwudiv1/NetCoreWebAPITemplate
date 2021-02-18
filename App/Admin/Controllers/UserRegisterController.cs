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
                var data = await this.services.RegisterPassport(registerInfo);
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
        /// <param name="RegisterInfo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> SendRegisterVerifyCodeByEmail([FromBody] DTOAPI_EmailVerifyCode emailInfo)
        {
            try
            {
                await this.services.SendRegisterVerifyCodeByEmail(emailInfo);
                return OkEx("");
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
                var data = await this.services.RegisterByEmailVerifyCode(registerInfo);
                return OkEx(data);
            }
            catch (Exception ex)
            {
                return this.JsonToCamelCase(ex.Message, 50000, 50000, ex.Message);
            }
        }

    }
}
