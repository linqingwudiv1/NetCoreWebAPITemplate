using BusinessAdminDLL.DTOModel.API.Roles;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAdminDLL.Roles
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRolesBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<DTOAPI_Role>> GetRoles();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<DTOAPI_Role> GetRole(long key);
        ///
        Task<int> AddRole(DTOAPI_Role data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> UpdateRole(DTOAPI_Role data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<int> DeleteRole(long Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        Task<int> DeleteRoles(IList<long> Ids);
    }
}
