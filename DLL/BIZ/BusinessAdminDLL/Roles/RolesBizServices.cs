﻿using AdminServices.Command.Role;
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
        protected IMapper mapper { get; set; }


        /// <summary>
        /// 
        /// </summary>
        protected readonly IPublishEndpoint publishEndpoint;

        /// <summary>
        /// 
        /// </summary>
        protected readonly ISendEndpoint sendEndpoint;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="_roleAccesser"></param>
        /// <param name="_mapper"></param>
        /// <param name="_publishEndpoint"></param>
        public RolesBizServices(IIDGenerator _IDGenerator, 
                                IRoleAccesser _roleAccesser, 
                                IMapper _mapper, 
                                IPublishEndpoint _publishEndpoint)
            : base()
        {
            this.accesser        = _roleAccesser;
            this.IDGenerator     = _IDGenerator;
            this.mapper          = _mapper;
            this.publishEndpoint = _publishEndpoint;
            //this.sendEndpoint    = _sendEndpoint;
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
        public Task<IList<DTOAPI_Role>> GetRoles()
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
                                         routes = GenPageRouteTree(x)
                                     }
                           ).ToArray();
            return Task.FromResult( roles);
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
                    routes = GenPageRouteTree(role)
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
        public Task<int> AddRole(DTOAPI_Role data)
        {
            Role role = new Role {
                Id = this.IDGenerator.GetNewID<Role>()  ,
                Descrption  = data.description          ,
                RoleName    = data.name                 ,
                DisplayName = data.name                 ,
                Organization = ""
            };

            return Task.FromResult(accesser.Add(role) );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task<int> UpdateRole(DTOAPI_Role data)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Task<int> DeleteRole(long Id)
        {
            this.publishEndpoint.Publish(new DeleteRoleCommand {key = Id }).Wait();
            return Task.FromResult(0) ;
            //return this.accesser.Delete(Id);
            //throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public Task<int> DeleteRoles(IList<long> Ids)
        { 
            return Task.FromResult(0) ;
        }
    }
}
