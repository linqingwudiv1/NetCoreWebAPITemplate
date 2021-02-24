using AdminServices.Command.Account;
using AdminServices.Command.Captcha;
using AutoMapper;
using BaseDLL.Helper.Captcha;
using BaseDLL.Helper.Smtp;
using BusinessCoreDLL.DTOModel.API.Users;
using BusinessCoreDLL.DTOModel.API.Users.ForgotPwd;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EFORM.Entity;
using DBAccessCoreDLL.Validator;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCoreDLL.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountRegisterBizServices : IAccountRegisterBizServices
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
        public AccountRegisterBizServices(IIDGenerator _IDGenerator,
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
        /// 发送验证码
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        public async Task SendRegisterVerifyCodeByEmail(DTOAPI_EmailVerifyCode emailInfo)
        {
            if (!EmailHepler.IsValid(emailInfo.email))
            {
                throw new Exception("邮箱格式不正确");
            }

            var account = this.accesser.Get(email: emailInfo.email).Item1;

            if (account != null)
            {
                throw new Exception("用户已存在");
            }

            await this.publishEndpoint.Publish(new SendRegisterEmailCapthcaCommand
            {
                key = $"RegisterCaptcha_{emailInfo.email.ToLower()}",
                email = emailInfo.email
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerInfo"></param>
        /// <returns></returns>
        public async Task<long> RegisterByEmailVerifyCode(DTOAPI_RegisterByEmailVerifyCode registerInfo)
        {
            if (!EmailHepler.IsValid(registerInfo.email))
            {
                throw new Exception("邮箱格式不正确");
            }

            if (!AccountValidator.bValidPassword(registerInfo.pwd))
            {
                throw new Exception("密码格式错误");
            }

            string key_captcha = $"RegisterCaptcha_{registerInfo.email.ToLower()}";
            string captcha = this.captchaHelper.GetCaptcha(key_captcha);

            if (String.IsNullOrEmpty(captcha) ||
                 !String.Equals(captcha, registerInfo.verifyCode, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("验证码错误");
            }

            var account = this.accesser.Get(email: registerInfo.email).Item1;
            if (account != null)
            {
                throw new Exception("用户已存在");
            }

            var newId = this.IDGenerator.GetNewID<Account>();
            await this.publishEndpoint.Publish(new RegisterAccountByEmailCommand 
            {
                id = newId,
                email = registerInfo.email.ToLower(),
                password = registerInfo.pwd
            });

            await this.publishEndpoint.Publish(new DeleteAccountCaptchaCommand
            {
                key = key_captcha
            });

            return newId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerInfo"></param>
        /// <returns></returns>
        public async Task<long> RegisterPassport(DTOAPI_RegisterByPassport registerInfo)
        {
            if ( !AccountValidator.bValidPassport(registerInfo.passport ))
            {
                throw new Exception("用户名格式不正确");
            }

            if (!AccountValidator.bValidPassword(registerInfo.password))
            {
                throw new Exception("密码格式错误");
            }

            var account = this.accesser.Get(passport: registerInfo.passport ).Item1;
            if (account != null)
            {
                throw new Exception("用户名已存在");
            }

            
            long newId = this.IDGenerator.GetNewID<Account>();
            await this.publishEndpoint.Publish(new RegisterAccountByPassportCommand
            {
                id = newId,
                passport = registerInfo.passport,
                password = registerInfo.password
            });


            return newId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        public async Task<bool> IsValidEmailCodeByRegister(DTOAPI_EmailVerifyCode emailInfo)
        {
            bool bSuccess = false;

            if (!EmailHepler.IsValid(emailInfo.email))
            {
                throw new Exception("邮箱格式不正确");
            }


            bSuccess = this.captchaHelper.IsValidCaptcha($"RegisterCaptcha_{emailInfo.email.ToLower()}", emailInfo.verifyCode);

            return bSuccess;
        }
    }
}
