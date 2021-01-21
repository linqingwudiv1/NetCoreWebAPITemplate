using BusinessAdminDLL.DTOModel.API.Roles;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using System.Collections.Generic;

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
        dynamic GetRoles();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        dynamic GetRole(long key);
        ///
        dynamic AddRole(DTOAPI_Role data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        dynamic UpdateRole(DTOAPI_Role data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        dynamic DeleteRole(long Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        dynamic DeleteRoles(IList<long> Ids);
    }
}
