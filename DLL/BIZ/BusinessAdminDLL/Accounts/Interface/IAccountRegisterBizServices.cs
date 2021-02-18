using BusinessAdminDLL.DTOModel.API.Users;
using BusinessAdminDLL.DTOModel.API.Users.ForgotPwd;
using System.Threading.Tasks;

namespace BusinessAdminDLL.Accounts
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
    }
}