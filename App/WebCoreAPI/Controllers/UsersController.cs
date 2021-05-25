using BaseDLL.DTO;
using BusinessCoreDLL.Accounts;
using BusinessCoreDLL.DTOModel.API.Users;
using DBAccessCoreDLL.DTO.API.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebCoreService.Controllers
{
    /// <summary>
    /// Vue 项目展示接口
    /// </summary>
    [Route("api/[controller]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class UsersController : BaseController
    {
        /// <summary>
        /// 用户接口
        /// </summary>
        private readonly IAccountsBizServices services;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Services"></param>
        public UsersController(IAccountsBizServices _Services)
        {
            services = _Services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> Info()
        {
            try
            {
                long userid = Int64.Parse(this.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault());
                
                var ret_model = await this.services.GetInfo(userid).ConfigureAwait(false);
                return JsonToCamelCase(ret_model);

            }
            catch (Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000);
            }
        }

        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> ChangeNickName([FromBody] DTOAPI_ChangeNickName info)
        {
            try
            {
                long userid = Int64.Parse(this.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault());
                await this.services.ChangeNickName(userid, info).ConfigureAwait(false);
                return JsonToCamelCase(new { success = true });

            }
            catch (Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> ChangeIntroduction([FromBody] DTOAPI_ChangeIntroduction info)
        {
            try
            {
                long userid = Int64.Parse(this.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault());
                await this.services.ChangeIntroduction(userid, info).ConfigureAwait(false);
                return JsonToCamelCase(new { success = true });
            }
            catch (Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        [Authorize]
        public async Task<IActionResult> ChangeAvatar([FromBody] DTOAPI_ChangeAvatar info) 
        {
            try
            {
                long userid = Int64.Parse(this.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault());
                await this.services.ChangeAvatar(userid, info).ConfigureAwait(false);
                return JsonToCamelCase(new { success = true });
            }
            catch (Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000);
            }
        }

        /// <summary>
        /// 获取COS 临时Token,有效期1800 second
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetCOSToken()
        {
            try
            {
                dynamic data = await this.services.GetCOSToken().ConfigureAwait(false);
                return JsonToCamelCase(data);
            }
            catch (Exception ex) 
            {
                return JsonToCamelCase(ex.Message, 50000, 50000);
            }
        }
    }
}