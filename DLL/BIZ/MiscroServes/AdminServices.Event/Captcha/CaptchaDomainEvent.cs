using AdminServices.Command.Account;
using AdminServices.Command.Captcha;
using BaseDLL.Helper.Captcha;
using BaseDLL.Helper.Smtp;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdminServices.Event.Captcha
{
    public class CaptchaDomainEvent 
        : 
        IConsumer<SendLoginEmailCapthcaCommand>,
        IConsumer<SendRegisterEmailCapthcaCommand>,
        IConsumer<SendFotgotPwdEmailCapthcaCommand>,
        IConsumer<DeleteAccountCaptchaCommand>
    {
        protected readonly ICaptchaHelper captchaHelper;
        /// <summary>
        /// 
        /// </summary>
        public CaptchaDomainEvent(ICaptchaHelper _captchaHelper) 
        {
            this.captchaHelper = _captchaHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<SendLoginEmailCapthcaCommand> context)
        {

            var data = context.Message;
            string captcha = this.captchaHelper.NewCaptcha(data.key, data.type,6, data.timeout );
            EmailHepler.SendEmail(data.email,"Qing-登录验证码",$"登录验证码[{captcha}],{ data.timeout / 60 }分钟内有效.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<SendRegisterEmailCapthcaCommand> context)
        {
            var data = context.Message;
            string captcha = this.captchaHelper.NewCaptcha(data.key, data.type, 6, data.timeout);
            EmailHepler.SendEmail(data.email, "Qing-注册验证码", $"注册验证码[{captcha}],{ data.timeout / 60 }分钟内有效.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<SendFotgotPwdEmailCapthcaCommand> context)
        {
            var data = context.Message;
            string captcha = this.captchaHelper.NewCaptcha(data.key, data.type, 6, data.timeout);
            EmailHepler.SendEmail(data.email, "Qing-找回密码验证码", $"忘记密码验证码[{captcha}],{ data.timeout / 60 }分钟内有效.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<DeleteAccountCaptchaCommand> context)
        {
            var data = context.Message;

            this.captchaHelper.DeleteCaptcha(data.key);

            //throw new NotImplementedException();
        }
    }
}
