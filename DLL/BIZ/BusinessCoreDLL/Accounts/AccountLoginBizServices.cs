using AdminServices.Command.Account;
using AdminServices.Command.Captcha;
using AutoMapper;
using BaseDLL.Helper.Captcha;
using BaseDLL.Helper.SMS;
using BaseDLL.Helper.Smtp;
using BusinessCoreDLL.DTOModel.API.Users;
using BusinessCoreDLL.DTOModel.API.Users.ForgotPwd;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EFORM.Entity;
using DBAccessCoreDLL.Validator;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCoreDLL.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountLoginBizServices : IAccountLoginBizServices
    {
        /// <summary>
        /// DAO层
        /// </summary>
        protected IAccountAccesser accesser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IIDGenerator IDGenerator { get; set; }


        /// <summary>
        /// 
        /// </summary>
        protected IMapper mapper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected readonly IPublishEndpoint publishEndpoint;

        /// <summary>
        /// 
        /// </summary>
        protected readonly ICaptchaHelper captchaHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="AccountAccesser"></param>
        /// <param name="_mapper"></param>
        /// <param name="_publishEndpoint"></param>
        /// <param name="_captchaHelper"></param>
        public AccountLoginBizServices(IIDGenerator _IDGenerator,
                                    IAccountAccesser AccountAccesser,
                                    IMapper _mapper,
                                    IPublishEndpoint _publishEndpoint,
                                    ICaptchaHelper _captchaHelper)
            : base()
        {
            this.accesser = AccountAccesser;
            this.IDGenerator = _IDGenerator;
            this.mapper = _mapper;
            this.publishEndpoint = _publishEndpoint;
            this.captchaHelper = _captchaHelper;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="LoginInfo"></param>
        /// <returns></returns>
        public async Task<dynamic> Login(DTOAPIReq_Login LoginInfo)
        {
            return LoginUnionWay(LoginInfo);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        public async Task SendLoginVerifyCodeByEmail(DTOAPI_EmailVerifyCode emailInfo)
        {
            if ( !EmailHepler.IsValid(emailInfo.email) ) 
            {
                throw new Exception("邮箱格式不正确");
            }
            var account = this.accesser.Get(email: emailInfo.email).Item1;

            if (account == null)
            {
                throw new Exception("用户不存在");
            }

            await this.publishEndpoint.Publish(new SendLoginEmailCapthcaCommand
            {
                key = $"LoginCaptcha_{emailInfo.email.ToLower()}",
                email = emailInfo.email
            });
        }


        /// <summary>
        /// 登录-使用Email验证码
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        public async Task<dynamic> LoginByEmailVerifyCode(DTOAPI_EmailVerifyCode emailInfo)
        {

            Account account = this.accesser.Get(email: emailInfo.email).Item1;
            
            if (account == null)
            {
                return new DTOAPIRes_Login
                {
                    accessToken = "",
                    state = 3,
                    msg = "用户不存在"
                };
            }

            string key_captcha = $"LoginCaptcha_{emailInfo.email}";

            if (!this.captchaHelper.IsValidCaptcha($"LoginCaptcha_{emailInfo.email}", emailInfo.verifyCode))
            {                                                       
                return new DTOAPIRes_Login
                {
                    accessToken = "",
                    state = 4,
                    msg = "验证码错误"
                };
            }

            await this.publishEndpoint.Publish(new DeleteAccountCaptchaCommand
            {
                key = key_captcha
            });

            return this.GenLoginData(account, account.Password);
        }



        #region private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="nickName"></param>
        /// <returns></returns>
        private DTOAPIRes_Login GenLoginData(Account account, string password, string nickName = "用户")
        {
            if (account != null && account.Password == password)
            {
                return new DTOAPIRes_Login
                {
                    accessToken = GenJWTToken(account),
                    refreshToken = Guid.NewGuid().ToString(),
                    state = 1,
                    expires = GJWT.Expires,
                    refreshExpires = GJWT.ExpiresRefresh,
                    msg = $"{nickName}登录成功"
                };
            }
            else
            {
                return new DTOAPIRes_Login
                {
                    accessToken = "",
                    state = 2,
                    msg = $"{nickName}密码错误"
                };
            }
        }

        /// <summary>
        /// 生成新JWT Token
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private string GenJWTToken(Account account)
        {
            DateTime ExpiresTime = DateTime.Now.AddMinutes(GJWT.Expires);
            Claim[] claims = new[]
            {
                    // 时间戳 
                    new Claim( JwtRegisteredClaimNames.Nbf,  $"{ new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds() }") ,
                    // 过期日期
                    new Claim( JwtRegisteredClaimNames.Exp,  $"{ new DateTimeOffset(ExpiresTime).ToUnixTimeSeconds() }"),
                    // 用户标识
                    new Claim( ClaimTypes.NameIdentifier, $"{account.Id}" ) ,
                    // 用户名
                    new Claim( ClaimTypes.Name, $"{account.DisplayName?? ""}"  ) , 
                    // 邮箱
                    new Claim( ClaimTypes.Email, $"{account.Email ?? ""}" ) ,
                    // 手机号
                    new Claim( ClaimTypes.MobilePhone, String.IsNullOrEmpty( account.PhoneAreaCode ) ? "" : $"{account.PhoneAreaCode}-{ account.Phone}" ) ,
                    // Custom Data
                    // new Claim("customType", "hi ! LinQing")
                };

            // Key
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GJWT.SecurityKey));

            // 加密方式
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: GJWT.Domain,
                audience: GJWT.Domain,
                claims: claims,
                expires: ExpiresTime,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        private DTOAPIRes_Login LoginUnionWay(DTOAPIReq_Login loginInfo)
        {

            var entity = this.accesser.db.Accounts;
            IQueryable<Account> query_union = this.accesser.db.Accounts.Where(x => 1 == 0);

            try
            {
                Int64 key = Int64.Parse(loginInfo.username);
                entity.Where(x => x.Id == key);
                query_union = query_union.Union(entity.Where(x => x.Id == key));
            }
            catch (Exception)
            {
            }

            if (PhoneHelper.IsValid(loginInfo.username))
            {
                var data = PhoneHelper.Split(loginInfo.username);

                var areacode = data.Item1;
                var phone = data.Item2;
                query_union = query_union.Union(entity.Where(x => x.PhoneAreaCode == areacode && x.Phone == phone));
            }

            if (EmailHepler.IsValid(loginInfo.username))
            {
                string email = loginInfo.username.ToLower();
                query_union = query_union.Union(entity.Where(x => x.Email.Equals(email) ));
            }

            query_union = query_union.Union(entity.Where(x => x.Username == loginInfo.username ));
            query_union = query_union.Union(entity.Where(x => x.Passport.Equals( loginInfo.username.ToLower() ) ));
#if DEBUG
#endif

            var arr = query_union.ToArray();
            if (arr != null && arr.Length > 0)
            {
                var account = arr.Where(x => x.Password == loginInfo.password).SingleOrDefault();
                return GenLoginData(account, loginInfo.password);
            }
            else
            {
                return new DTOAPIRes_Login
                {
                    accessToken = "",
                    state = 3,
                    msg = "UID/通行证/用户名/邮箱/手机号不存在"
                };

            }

            //return account;
        }

        #endregion
    }
}
