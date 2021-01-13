using BusinessCoreDLL.DTOModel.API.Roles;
using DBAccessCoreDLL.EF.Context;
using DBAccessCoreDLL.EF.Entity;

namespace BusinessCoreDLL.Roles
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
        Role[] GetRoles();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Role GetRole(long key);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
    }
}
