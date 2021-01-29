using BaseDLL.DTO;
using BaseDLL.Helper;
using BaseDLL.Helper.SMS;
using BaseDLL.Helper.Smtp;
using BusinessAdminDLL.Base;
using BusinessAdminDLL.DTOModel.API.Users;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using MassTransit.Internals.Reflection;
using Microsoft.IdentityModel.Tokens;
using NetApplictionServiceDLL;
using ServiceStack;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAdminDLL.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    class AccountBizServices : BaseBizServices, IAccountsBizServices
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
        /// <param name="_IDGenerator"></param>
        /// <param name="AccountAccesser"></param>
        public AccountBizServices(IIDGenerator _IDGenerator, IAccountAccesser AccountAccesser)
            : base()
        {
            this.accesser = AccountAccesser;
            this.IDGenerator = _IDGenerator;
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RegisterAccountInfo Register(DTOAPIReq_Register model)
        {
            RegisterAccountInfo RegisterInfo = RegisterAccountVerify(model);

            //注册未成功.....
            if (RegisterInfo.State != ERegisterAccountState.Success)
            {
                return RegisterInfo;
            }

            //next
            bool isExistAccount = this.IsRegisterAccountExisted(model, ref RegisterInfo);

            if (isExistAccount)
            {
                return RegisterInfo;
            }

            long NewID = IDGenerator.GetNewID<Account>();
            string NewUsername = model.Username ?? @$"User_{NewID.ToString()}";

            Account account = new Account
            {
                Id = NewID,
                Passport = model.Passport,
                Username = NewUsername,
                Email = model.EMail,
                PhoneAreaCode = "86",
                Phone = model.Phone,
                Sex = -1,
                // md5 secret key
                Password = model.Password   //MD5Helper.GetMd5Hash(  )
            };

            accesser.Add(account);
            return RegisterInfo;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        private DTOAPIRes_Login GenLoginData(Account account, DTOAPIReq_Login loginInfo) 
        {
            if ( account.Password == loginInfo.password )
            {
                return new DTOAPIRes_Login
                {
                    accessToken = GenJWTToken(account),
                    state = 1,
                    msg = "用户登录成功"
                };
            }
            else 
            {
                return new DTOAPIRes_Login
                {
                    accessToken = "",
                    state = 2,
                    msg = "密码错误"
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
            DateTime ExpiresTime = DateTime.Now.AddMinutes(30);
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

        private dynamic TryLoginByID(DTOAPIReq_Login loginInfo)
        {
            // loginInfo.
            try
            {
                Int64 key = Int64.Parse(loginInfo.passport);
                var account = this.accesser.Get(key: key);
                
                if (account.Item1 != null &&
                    account.Item1.Password == loginInfo.password )
                {
                    return GenLoginData(account.Item1, loginInfo);
                }
                else
                {
                    //不匹配时尝试手机号登录
                    return TryLoginByPhone(loginInfo);
                }
            }
            catch (Exception ex) 
            {
                return TryLoginByPhone(loginInfo);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        private dynamic TryLoginByPhone(DTOAPIReq_Login loginInfo)
        {
            if (PhoneHelper.IsValid(loginInfo.passport))
            {
                var data = PhoneHelper.Split(loginInfo.passport);
                
                var areacode = data.Item1;
                var phone = data.Item2;

                var account = ( from 
                                    x 
                                in 
                                    this.accesser.db.Accounts 
                                where 
                                    x.PhoneAreaCode == areacode && 
                                    x.Phone == phone 
                                select x).SingleOrDefault(null);

                if (account != null && 
                    account.Password == loginInfo.password)
                {
                    return GenLoginData(account, loginInfo);
                }
                else 
                {
                    // 查找不到手机号时,尝试通行证登录
                    return TryLoginByPassport(loginInfo);
                }
            }
            else 
            {
                return TryLoginByEmail(loginInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        private dynamic TryLoginByEmail(DTOAPIReq_Login loginInfo)
        {
            if (EmailHepler.IsValid(loginInfo.passport))
            {
                string email = loginInfo.passport;
                var account = (from
                                   x
                               in
                                   this.accesser.db.Accounts
                               where
                                   x.Email == email
                               select x).SingleOrDefault();

                if (account != null && 
                    account.Password == loginInfo.password)
                {
                    return GenLoginData(account, loginInfo);
                }
                else 
                {
                    // 查找不到手机号时,尝试通行证登录
                    return TryLoginByPassport(loginInfo);
                }
            }
            else 
            {
                return TryLoginByPassport(loginInfo);
            }
        }

        private dynamic TryLoginByPassport(DTOAPIReq_Login loginInfo) 
        {
            var account = (from
                               x
                           in
                               this.accesser.db.Accounts
                           where
                               x.Username == loginInfo.passport
                           select x).SingleOrDefault();


            if ( account != null )
            {
                if ( account.Password == loginInfo.password )
                {

                    return new DTOAPIRes_Login 
                    {
                        accessToken = GenJWTToken(account)  ,
                        state = 1                           ,
                        msg = "登录成功"
                    };
                }
                else
                {
                    return LoginByUserName(loginInfo);
                }
            }
            else
            {
                return LoginByUserName(loginInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        private dynamic LoginByUserName(DTOAPIReq_Login loginInfo) 
        {

            Account account = (from
                                   x
                               in
                                   this.accesser.db.Accounts
                               where
                                   x.Username == loginInfo.passport
                               select x).SingleOrDefault();

            if (account != null)
            {
                if (account.Password == loginInfo.password)
                {
                    return new DTOAPIRes_Login
                    {
                        accessToken = GenJWTToken(account),
                        state = 1,
                        msg = "登录成功"
                    };
                }
                else 
                {
                    return new DTOAPIRes_Login
                    {
                        accessToken = "",
                        state = 2,
                        msg = "密码错误"
                    };
                }
            }
            else 
            {
                return new DTOAPIRes_Login
                {
                    accessToken = ""    ,
                    state = 3           ,
                    msg = "UID/通行证/用户名/邮箱/手机号不存在"
                };
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="LoginInfo"></param>
        /// <returns></returns>
        public async Task<dynamic> Login(DTOAPIReq_Login LoginInfo)
        {
            return TryLoginByID(LoginInfo);
        }

        #region private

        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="model"></param>
        private RegisterAccountInfo RegisterAccountVerify(DTOAPIReq_Register model)
        {
            RegisterAccountInfo ret_model = new RegisterAccountInfo { account = null, State = ERegisterAccountState.Success, Message = "" };

            if (!string.IsNullOrWhiteSpace(model.EMail) && !EmailHepler.IsValid(model.EMail))
            {
                ret_model.State = ERegisterAccountState.FormatNotMatch;
                ret_model.Message = "无效的邮箱";
            }

            if (!string.IsNullOrWhiteSpace(model.Password) && !PasswordHelper.IsValid(model.Password))
            {
                ret_model.State = ERegisterAccountState.FormatNotMatch;
                ret_model.Message = "无效的密码格式";
            }

            if (!string.IsNullOrWhiteSpace(model.Phone) && !PhoneHelper.IsValid(model.Phone))
            {
                ret_model.State = ERegisterAccountState.FormatNotMatch;
                ret_model.Message = "无效的手机号码";
            }

            if (!string.IsNullOrWhiteSpace(model.Username) && !PhoneHelper.IsValid(model.Username))
            {
                ret_model.State = ERegisterAccountState.FormatNotMatch;
                ret_model.Message = "无效的用户昵称";
            }

            if (!string.IsNullOrWhiteSpace(model.Passport) && !PhoneHelper.IsValid(model.Passport))
            {
                ret_model.State = ERegisterAccountState.FormatNotMatch;
                ret_model.Message = "无效的用户名";
            }

            return ret_model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="RegisterInfo"></param>
        private bool IsRegisterAccountExisted(DTOAPIReq_Register model,ref RegisterAccountInfo RegisterInfo) 
        {
            // next
            Tuple<Account, EFindAccountWay> FindAccountResult = accesser.Get( passport: model.Passport,
                                                                              username: model.Username,
                                                                                 email: model.EMail,
                                                                                 phone: model.Phone     );
            // Verify Account whether exist...
            switch (FindAccountResult.Item2)
            {
                case EFindAccountWay.Id:
                    {
                        //ID is exist
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "ID已存在";
                        break;
                    }
                case EFindAccountWay.UserName:
                    {
                        //username is exist.
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "用户名已被注册";
                        break;
                    }
                case EFindAccountWay.Passport:
                    {
                        //passport is exist
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "通行证已被注册";
                        break;
                    }
                case EFindAccountWay.EMail:
                    {
                        //email is exist
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "邮箱已被注册";
                        break;
                    }
                case EFindAccountWay.Phone:
                    {
                        //phone is exist
                        RegisterInfo.State = ERegisterAccountState.ExistAccount;
                        RegisterInfo.Message = "手机号已被注册";
                        break;
                    }
                case EFindAccountWay.NotFound:
                default:
                {
                    RegisterInfo.State = ERegisterAccountState.Success;
                    RegisterInfo.Message = "Success";
                    break;
                }
            }

            return (FindAccountResult.Item2 == null);
        }
        #endregion

    }
}
