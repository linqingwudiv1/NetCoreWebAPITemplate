using BaseDLL.DTO;
using BusinessAdminDLL.DTOModel.API.Users;
using DBAccessCoreDLL.DTO.API.Users;
using DBAccessCoreDLL.EFORM.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdminDLL.Accounts
{


    /// <summary>
    /// 
    /// </summary>
    public enum ERegisterAccountState
    {
        /// <summary>
        /// 
        /// </summary>
        Error,
        /// <summary>
        /// 
        /// </summary>
        Success,
        /// <summary>
        /// 
        /// </summary>
        ExistAccount,
        /// <summary>
        /// 注册资料不匹配规则
        /// </summary> 
        FormatNotMatch
    }

    /// <summary>
    /// 
    /// </summary>
    public class RegisterAccountInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public RegisterAccountInfo()
        {
            this.State = ERegisterAccountState.Success;
        }

        /// <summary>
        /// 
        /// </summary>
        public Account account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ERegisterAccountState State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }
    }

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

        /// <summary>
        /// 获取用户的权限
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<IList<long>> GetAdminPageRoles(long userid);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DTO_PageableModel<DTOAPIRes_UserInfo>> GetUsers(DTO_PageableQueryModel<DTO_GetUsers> query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task UpdateUsersRole(DTOAPIReq_UpdateUsersRole info);
        Task<dynamic> GetCOSToken();
    }
}
