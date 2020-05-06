using System;
using System.Net;
using System.Net.Mail;

namespace BaseDLL.Helper.Smtp
{
    /// <summary>
    /// Smtp Helper
    /// </summary>
    public static class EmailHepler
    {

        /// <summary>
        /// 是否是有效的邮箱
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <returns></returns>
        static public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="titile"></param>
        /// <param name="content"></param>
        public static void SendEmail(string mail, string titile, string content)
        {
            MailMessage mailMsg = new MailMessage();

            // 源邮件地址 ,发件人
            mailMsg.From = new MailAddress("aa875191946@qq.com");
            
            // 收件人
            mailMsg.To.Add(new MailAddress(mail));
            
            // 邮件标题 
            mailMsg.Subject = titile;
            
            // 邮件内容 
            mailMsg.Body = content;
            mailMsg.IsBodyHtml = true;

            //发件人使用的邮箱的SMTP服务器。
            SmtpClient client = new SmtpClient("smtp.qq.com");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;

            // 指定发件人的邮箱的账号与密码.
            client.Credentials = new NetworkCredential("aa875191946@qq.com", "password");
            // 排队发送邮件.
            client.Send(mailMsg);
        }
    }
}
