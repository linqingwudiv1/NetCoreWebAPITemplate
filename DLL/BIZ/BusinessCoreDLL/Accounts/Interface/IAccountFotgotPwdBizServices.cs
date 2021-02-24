using BusinessCoreDLL.DTOModel.API.Users;
using BusinessCoreDLL.DTOModel.API.Users.ForgotPwd;
using System.Threading.Tasks;

namespace BusinessCoreDLL.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountFotgotPwdBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        Task SendForgotPwdVerifyCodeByEmail(DTOAPI_EmailVerifyCode emailInfo);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pwdInfo"></param>
        /// <returns></returns>
        Task ForgotPwdCodeByEmail(DTOAPI_ForgotPwdByEmailCaptcha pwdInfo);
        Task<bool> IsValidEmailCodeByForgotPwd(DTOAPI_ForgotPwdByEmailCaptcha pwdInfo);
    }
}