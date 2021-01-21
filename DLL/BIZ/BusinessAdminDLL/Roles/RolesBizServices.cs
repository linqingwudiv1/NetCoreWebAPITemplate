using BaseDLL.Helper;
using BusinessAdminDLL.Base;
using BusinessAdminDLL.DTOModel.API.Roles;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EFORM.Entity;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System.Collections.Generic;
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
        /// <param name="_roleAccesser"></param>
        public RolesBizServices(IIDGenerator _IDGenerator, IRoleAccesser _roleAccesser)
            : base()
        {
            this.accesser    = _roleAccesser;
            this.IDGenerator = _IDGenerator;
        }

        #region private

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        static private IList<DTOAPI_RoutePages> GenPageRouteTree(Role role) 
        {
            IList<DTOAPI_RoutePages> routes = new List<DTOAPI_RoutePages>();
            if ( role.RouteRoles != null && role.RouteRoles.Count > 0 ) 
            {

                routes = role.RouteRoles.Select(x => new DTOAPI_RoutePages 
                { 
                    id = x.routePage.Id                         , 
                    parentId = x.routePage.ParentId             , 
                    hierarchyPath = x.routePage.HierarchyPath   ,
                    component = x.routePage.Component           ,
                    name = x.routePage.RouteName                ,
                    path = x.routePage.Path                     ,
                    meta = new DTOAPI_RoutePagesMeta 
                    {
                        title = x.routePage.Title,
                        activeMenu = x.routePage.ActiveMenu,
                        affix = x.routePage.Affix,
                        alwaysShow = x.routePage.AlwaysShow,
                        hidden = x.routePage.Hidden,
                        icon = x.routePage.Icon,
                        noCache = x.routePage.NoCache
                    }
                }).GenerateTree(x => x.id, x => x.parentId,(n, children) => 
                {
                    n.children = children.ToArray();
                }, null).ToList();
            }

            return routes;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public dynamic GetRoles()
        {
            dynamic roles = (    from 
                                    x 
                                 in 
                                    this.accesser.db.Roles.Include( role => role.RouteRoles).ThenInclude( routepage => routepage.routePage)
                                 select
                                     new DTOAPI_Role
                                     {
                                         key = x.Id,
                                         description = x.Descrption,
                                         name = x.RoleName,
                                         routes = GenPageRouteTree(x)
                                     }
                           ).ToArray();

            return roles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public dynamic GetRole(long key)
        {
            Role role = (from 
                            x 
                         in 
                            this.accesser.db.Roles.Include(role => role.RouteRoles).ThenInclude(routepage => routepage.routePage)
                         where 
                            x.Id == key 
                         select x ).FirstOrDefault();

            if (role != null)
            {
                return new DTOAPI_Role
                {
                    key = role.Id,
                    description = role.Descrption,
                    name = role.RoleName,
                    routes = GenPageRouteTree(role)
                };
            }

            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public dynamic AddRole(DTOAPI_Role data)
        {
            Role role = new Role {
                Id = this.IDGenerator.GetNewID<Role>()  ,
                Descrption = data.description           ,
                RoleName = data.name                    ,
                DisplayName = data.name                 ,
                Organization = ""
            };

            return accesser.Add(role);
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
            return this.accesser.Delete(Id);
            //throw new System.NotImplementedException();
        }
    }
}
