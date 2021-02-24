using BusinessCoreDLL.DTOModel.API.Users;
using DBAccessCoreDLL.EFORM.Entity;
using System.Threading.Tasks;

namespace BusinessCoreDLL.Accounts
{

    /// <summary>
    /// 
    /// </summary>
    public interface IAccountsBizServices
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public Task<dynamic> GetInfo(long accountID);
        Task<dynamic> ChangeIntroduction(long userid, DTOAPI_ChangeIntroduction info);
        Task<dynamic> ChangeNickName(long userid, DTOAPI_ChangeNickName info);
    }
}
