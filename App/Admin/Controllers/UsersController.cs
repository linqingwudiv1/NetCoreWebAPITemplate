using BaseDLL.DTO.Store;
using BusinessAdminDLL.DTOModel.API.Users;
using BusinessAdminDLL.Extensison;
using BusinessAdminDLL.Accounts;
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

namespace WebAdminService.Controllers
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
        /// 用户接口
        /// </summary>
        public UsersController( IAccountsBizServices _Services)
        {
            services = _Services;
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="userInfo">登录信息</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTOAPIReq_Login userInfo)
        {
            try
            {
                return JsonToCamelCase(await this.services.Login(userInfo)) ;
            }
            catch (Exception ex)
            {
                return JsonToCamelCase(ex.Message, 50000, 50000);
            }
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
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
                    new Claim("customType", "hi ! LinQing")
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
                    accessToken = new JwtSecurityTokenHandler().WriteToken(token)
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
        [HttpPost("[action]")]
        public IActionResult Register([FromBody] DTOAPIReq_Register RegisterInfo)
        {
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
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
        [HttpPost("logout")]
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
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> Info()
        {
            long userid = Int64.Parse( this.User.Claims.Where(x=> x.Type == ClaimTypes.NameIdentifier).Select( x=> x.Value).FirstOrDefault());
            //t.ToList()[0].Claims.get
            try
            {
                return JsonToCamelCase(await this.services.GetInfo(1));
                
            }
            catch (Exception ex) 
            {
                return JsonToCamelCase(ex.Message ,50000,50000);
            }
        }
    }
}
