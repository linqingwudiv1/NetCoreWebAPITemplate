using Bogus.DataSets;
using BusinessAdminDLL.Base;
using BusinessAdminDLL.DTOModel.API.Roles;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EF.Entity;
using System.Linq;

namespace BusinessAdminDLL.Roles
{
    /// <summary>
    /// 
    /// </summary>
    public class RolesBizServices : BaseBizServices, IRolesBizServices
    {
        
        /// <summary>
        /// DAO层
        /// </summary>
        protected IRoleAccesser accesser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IIDGenerator IDGenerator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="RoleAccesser"></param>
        public RolesBizServices(IIDGenerator _IDGenerator, IRoleAccesser RoleAccesser)
            : base()
        {
            this.accesser    = RoleAccesser;
            this.IDGenerator = _IDGenerator;
        }

        #region private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void ConvertNodeToRoute(DTOAPI_RoutePages data)
        {
            // this.printTitle(node.title)
            // foreach (Node child in data.child  .children)
            // {
            //     printNode(child); //<-- recursive
            // }
        }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Role[] GetRoles()
        {
            Role[] roles = this.accesser.db.Roles.ToArray();
            return roles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Role GetRole(long key)
        {
            Role role = this.accesser.Get(key);
            return role;
            
            // throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public dynamic AddRole(DTOAPI_Role data)
        {

            Role role = new Role {
                Id = this.IDGenerator.GetNewID<Role>(),
                Descrption = data.description,
                RoleName = data.name,
                DisplayName = data.name,
                Organization = ""
            };


            return accesser.Add(role);
            //throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public dynamic UpdateRole(DTOAPI_Role data)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public dynamic DeleteRole(long Id)
        {
            throw new System.NotImplementedException();
        }
    }
}
