using BusinessCoreDLL.DTOModel.API.Users;
using BusinessCoreDLL.DTOModel.API.Users.ForgotPwd;
using System.Threading.Tasks;

namespace BusinessCoreDLL.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountLoginBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        Task<dynamic> Login(DTOAPIReq_Login loginInfo);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        Task SendLoginVerifyCodeByEmail(DTOAPI_EmailVerifyCode emailInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        Task<dynamic> LoginByEmailVerifyCode(DTOAPI_EmailVerifyCode emailInfo);
    }
}