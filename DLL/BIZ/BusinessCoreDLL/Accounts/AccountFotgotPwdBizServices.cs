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
    public class AccountFotgotPwdBizServices : IAccountFotgotPwdBizServices
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

        ///
        protected readonly ICaptchaHelper captchaHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="AccountAccesser"></param>
        /// <param name="_mapper"></param>
        /// <param name="_publishEndpoint"></param>
        /// <param name="_captchaHelper"></param>
        public AccountFotgotPwdBizServices(IIDGenerator _IDGenerator,
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
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        public async Task SendForgotPwdVerifyCodeByEmail(DTOAPI_EmailVerifyCode emailInfo)
        {
            if ( !EmailHepler.IsValid(emailInfo.email))
            {
                throw new Exception("邮箱格式不正确");
            }

            var account = this.accesser.Get(email: emailInfo.email).Item1;

            if (account == null)
            {
                throw new Exception("用户不存在");
            }

            await this.publishEndpoint.Publish(new SendFotgotPwdEmailCapthcaCommand 
            {
                key = $"ForgotPwdCaptcha_{emailInfo.email.ToLower()}",
                email = emailInfo.email.ToLower()
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pwdInfo"></param>
        /// <returns></returns>
        public async Task ForgotPwdCodeByEmail(DTOAPI_ForgotPwdByEmailCaptcha pwdInfo)
        {
            if (!EmailHepler.IsValid(pwdInfo.email))
            {
                throw new Exception("邮箱格式不正确");
            }

            if (!AccountValidator.bValidPassword( pwdInfo.newpwd)) 
            {
                throw new Exception("密码格式不正确");
            }

            var account = this.accesser.Get(email: pwdInfo.email).Item1;

            if (account == null)
            {
                throw new Exception("用户不存在");
            }

            string key_captcha = $"ForgotPwdCaptcha_{pwdInfo.email.ToLower()}";

            if (!this.captchaHelper.GetCaptcha(key_captcha).Equals(pwdInfo.verifyCode, StringComparison.CurrentCultureIgnoreCase) ) 
            {
                throw new Exception("验证码错误");
            }

            await this.publishEndpoint.Publish(new ChangePasswordCommand
            {
                key = account.Id,
                newPassword = pwdInfo.newpwd
            });

            await this.publishEndpoint.Publish(new DeleteAccountCaptchaCommand
            {
                key = key_captcha
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pwdInfo"></param>
        /// <returns></returns>
        public async Task<bool> IsValidEmailCodeByForgotPwd(DTOAPI_ForgotPwdByEmailCaptcha pwdInfo)
        {
            bool bSuccess = false;

            if (!EmailHepler.IsValid(pwdInfo.email))
            {
                throw new Exception("邮箱格式不正确");
            }


            bSuccess = this.captchaHelper.IsValidCaptcha($"ForgotPwdCaptcha_{pwdInfo.email.ToLower()}", pwdInfo.verifyCode);

            return bSuccess;
        }
    }
}
