using BaseDLL.DTO.Store;
using BusinessCoreDLL.DTOModel.API.Users;
using BusinessCoreDLL.Extensison;
using BusinessCoreDLL.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetApplictionServiceDLL;
using NetApplictionServiceDLL.Filter;
using System;
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
    [Route("Api/[controller]/[action]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class UsersController : BaseController
    {
        /// <summary>
        /// 用户接口
        /// </summary>
        private readonly IAccountsBizServices accountServices;

        /// <summary>
        /// 用户接口
        /// </summary>
        public UsersController( IAccountsBizServices _accountServices)
        {
            accountServices = _accountServices;
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="userInfo">登录信息</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login([FromBody] DTOAPIReq_Login userInfo)
        {
            return Ok(userInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult JWTLogin([FromBody]DTOAPIReq_Login userInfo)
        {
            if (userInfo == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(userInfo.passport) && !string.IsNullOrEmpty(userInfo.password))
            {
                Claim[] claims = new[]
                {
                    // 时间戳
                    new Claim( JwtRegisteredClaimNames.Nbf,  $"{ new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() }") ,
                    
                    // 过期日期
                    new Claim( JwtRegisteredClaimNames.Exp,  $"{ new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds() }"),
                    
                    // 用户标识
                    new Claim( ClaimTypes.Name, userInfo.passport ) , 

                    // Custom Data
                    new Claim("customType", "hi! LinQing")
                };

                // Key
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GJWT.SecurityKey));

                // 加密方式
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                
                // Token
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer   : GJWT.Domain ,
                    audience : GJWT.Domain ,
                    claims   : claims ,
                    expires  : DateTime.Now.AddMinutes(30) ,
                    signingCredentials : creds );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            else
            {
                return BadRequest(new { message = "username or password is incorrect." });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RegisterInfo"></param>
        /// <returns></returns>
        public async Task<IActionResult> Register([FromBody] DTOAPIReq_Register RegisterInfo) 
        {
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> JWTTestAsync() 
        {
            AuthenticateResult result = await this.HttpContext.AuthenticateAsync().ConfigureAwait(true);

            if (result.Principal.Claims != null && result.Principal.Claims.Any())
            {
                Claim customType = ( from x in result.Principal.Claims.DefaultIfEmpty() where x.Type == "customType" select x ).FirstOrDefault(null);

                return Ok($"User Claim : { result.Principal.Identity.Name } , customType :{ customType.Value }");
            }
            else 
            {
                return Ok($"过期");
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AuthFilter]
        public IActionResult Logout()
        {
            this.LogoutLogic();
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthFilter]
        public IActionResult Info([FromBody] DTOAPIReq_Info Info)
        {
            DTO_StoreAccount store_account = this.GetStoreAccount();

            return Ok(store_account);
        }
    }
}
