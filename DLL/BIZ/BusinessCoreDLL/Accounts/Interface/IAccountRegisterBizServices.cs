using BusinessCoreDLL.DTOModel.API.Users;
using BusinessCoreDLL.DTOModel.API.Users.ForgotPwd;
using System.Threading.Tasks;

namespace BusinessCoreDLL.Accounts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountRegisterBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<long> RegisterPassport(DTOAPI_RegisterByPassport registerInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        Task SendRegisterVerifyCodeByEmail(DTOAPI_EmailVerifyCode emailInfo);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerInfo"></param>
        /// <returns></returns>
        Task<long> RegisterByEmailVerifyCode(DTOAPI_RegisterByEmailVerifyCode registerInfo);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailInfo"></param>
        /// <returns></returns>
        Task<bool> IsValidEmailCodeByRegister(DTOAPI_EmailVerifyCode emailInfo);
    }
}