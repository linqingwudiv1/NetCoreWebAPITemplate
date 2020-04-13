using BusinessDLL.Extensison;
using DTOModelDLL.API.Users;
using DTOModelDLL.Common;
using DTOModelDLL.Common.Store;
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

namespace AdminService.Controllers
{
    /// <summary>
    /// Vue项目展示接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class UsersController : BaseController
    {
        /// <summary>
        /// Vue项目测试接口
        /// </summary>
        public UsersController()
        {
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="userInfo">登录信息</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login([FromBody] DTOAPI_Login userInfo)
        {
            return Ok(new DTO_ReturnModel<dynamic>(this.LoginLogic(userInfo), 20000));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult JWTLogin( [FromBody]DTOAPI_Login userInfo)
        {
            if (userInfo == null)
            {
                return NotFound();
            }
            
            if (!string.IsNullOrEmpty(userInfo.username) && !string.IsNullOrEmpty(userInfo.password))
            {
                Claim[] claims = new[]
                {
                    // 时间戳
                    new Claim( JwtRegisteredClaimNames.Nbf,  $"{ new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    // 过期日期
                    new Claim( JwtRegisteredClaimNames.Exp,  $"{ new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                    // 
                    new Claim( ClaimTypes.Name, userInfo.username ) ,
                    // Custom Data
                    new Claim("customType", "hi!linqing")
                };

                // Key
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GJWT.SecurityKey));

                // 加密方式
                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //
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
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> JWTTestAsync() 
        {
            AuthenticateResult result = await this.HttpContext.AuthenticateAsync().ConfigureAwait(true);

            if ( result.Principal.Claims != null && result.Principal.Claims.Any() )
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
            return Ok(new DTO_ReturnModel<string>(null, 20000));
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

            return Ok(new DTO_ReturnModel<dynamic>(store_account, 20000));
        }
    }
}
