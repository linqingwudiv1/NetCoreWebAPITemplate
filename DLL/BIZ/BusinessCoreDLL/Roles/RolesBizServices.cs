using Bogus.DataSets;
using BusinessCoreDLL.Base;
using BusinessCoreDLL.DTOModel.API.Roles;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EF.Entity;
using System.Linq;

namespace BusinessCoreDLL.Roles
{
    /// <summary>
    /// 
    /// </summary>
    public class RolesBizServices : BaseBizServices, IRolesBizServices
    {
        /// <summary>
        /// DAO层
        /// </summary>
        protected IRoutePageAccesser accesser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IIDGenerator IDGenerator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="RoleAccesser"></param>
        public RolesBizServices(IIDGenerator _IDGenerator, IRoutePageAccesser RoleAccesser)
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
        private void printNode(DTOAPI_Role data)
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
            return this.accesser.db.Roles.ToArray();
            //return;//this.accesser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Role GetRole(long key)
        {
            return this.accesser.Get(key);
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
