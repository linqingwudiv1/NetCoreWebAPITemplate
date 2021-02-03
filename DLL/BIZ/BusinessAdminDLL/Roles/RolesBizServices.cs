using AdminServices.Command.Role;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BaseDLL.Helper;
using BusinessAdminDLL.Base;
using BusinessAdminDLL.DTOModel.API.Roles;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EFORM.Entity;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAdminDLL.Roles
{

    /// <summary>
    /// 
    /// </summary>
    public static class RolesBizServicesExtension 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        static public IList<DTOAPI_RoutePages> GenPageRouteTree( this Role role)
        {
            IList<DTOAPI_RoutePages> routes = new List<DTOAPI_RoutePages>();
            if (role.RouteRoles != null && role.RouteRoles.Count > 0)
            {

                routes = role.RouteRoles.Select(x => new DTOAPI_RoutePages
                {
                    id = x.routePage.Id,
                    parentId = x.routePage.ParentId,
                    hierarchyPath = x.routePage.HierarchyPath,
                    component = x.routePage.Component,
                    name = x.routePage.RouteName,
                    //path  = "",
                    path = x.routePage.Path,
                    redirect = x.routePage.Redirect,
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
                }).GenerateTree(x => x.id, x => x.parentId, (n, children) =>
                {
                    n.children = (children.Count() > 0 ? children.ToArray() : null);
                }, null).ToList();
            }

            return routes;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RolesBizServices : BaseBizServices ,IRolesBizServices
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
        protected IMapper mapper { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected readonly IPublishEndpoint publishEndpoint;


        protected readonly IRequestClient<DeleteRoleCommand> deleteClient;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="_roleAccesser"></param>
        /// <param name="_mapper"></param>
        /// <param name="_publishEndpoint"></param>
        /// <param name="_deleteClient"></param>
        public RolesBizServices(IIDGenerator _IDGenerator, 
                                IRoleAccesser _roleAccesser, 
                                IMapper _mapper, 
                                IPublishEndpoint _publishEndpoint,
                                IRequestClient<DeleteRoleCommand> _deleteClient)
            : base()
        {
            this.accesser        = _roleAccesser;
            this.IDGenerator     = _IDGenerator;
            this.mapper          = _mapper;
            this.publishEndpoint = _publishEndpoint;
            this.deleteClient    = _deleteClient;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<DTOAPI_Role>> GetRoles()
        {
            IList<DTOAPI_Role> roles = (    from 
                                    x 
                                 in 
                                    this.accesser.db.Roles.Include( role => role.RouteRoles).ThenInclude( routepage => routepage.routePage)
                                 select
                                     new DTOAPI_Role
                                     {
                                         key = x.Id,
                                         description = x.Descrption,
                                         name = x.RoleName,
                                         routes = x.GenPageRouteTree()
                                     }
                           ).ToArray();

            return roles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<DTOAPI_Role> GetRole(long key)
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
                dynamic Role = new DTOAPI_Role
                {
                    key = role.Id,
                    description = role.Descrption,
                    name = role.RoleName,
                    routes = role.GenPageRouteTree()
                };
                return Task.FromResult( Role);
            }

            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<dynamic> AddRole(DTOAPIReq_Role data)
        {

            if (data.pageRoutes != null && data.pageRoutes.Count > 0) 
            {
                //ensure pageRoutes is exist
                if (this.accesser.db.RoutePages.Where(x => data.pageRoutes.Contains(x.Id)).Count() != data.pageRoutes.Count) 
                {
                    return -1;
                }
            }


            var routes = data.pageRoutes.Select(x => new DTOIn_PageRouteId { PageRouteID = x }).ToArray();

            long NewID = this.IDGenerator.GetNewID<Role>();
            var cmd = new AddRoleCommand
            {
                key = NewID,
                description = data.description,
                name = data.name,
                routes = routes
            };
            await this.publishEndpoint.Publish(cmd);
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<dynamic> UpdateRole(DTOAPIReq_Role data)
        { 
            
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<dynamic> DeleteRole(long Id)
        {
            await this.publishEndpoint.Publish(new DeleteRoleCommand { key = Id });
            return 1;
            //return this.accesser.Delete(Id);
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public async Task<dynamic> DeleteRoles(IList<long> Ids)
        {
            throw new System.NotImplementedException();
        }

    }
}
