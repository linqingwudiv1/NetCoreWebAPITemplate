using AdminServices.Command.PageRouteRole;
using AutoMapper;
using BaseDLL.Helper;
using BusinessAdminDLL.Base;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EFORM.Entity;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using RoutePage_Alias = DBAccessCoreDLL.EFORM.Entity.RoutePages;
namespace BusinessAdminDLL.RoutePage
{
    /// <summary>
    /// 
    /// </summary>
    public class RoutePageBizServices : BaseBizServices, IRoutePageBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        protected IIDGenerator IDGenerator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        readonly IRoutePageAccesser accesser;

        /// <summary>
        /// 
        /// </summary>
        readonly IPublishEndpoint publishEndpoint;

        readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="_accesser"></param>
        /// <param name="_publishEndpoint"></param>
        /// <param name="_mapper"></param>
        public RoutePageBizServices(IIDGenerator _IDGenerator, 
                                    IRoutePageAccesser _accesser,
                                    IPublishEndpoint _publishEndpoint,
                                    IMapper _mapper)
            : base()
        {
            this.IDGenerator = _IDGenerator;
            this.accesser = _accesser;
            this.publishEndpoint = _publishEndpoint;
            this.mapper = _mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<DTOAPIRes_RoutePages> GetRoutePages()
        {
            var List = (from 
                            x 
                        in 
                            this.accesser.db.RoutePages.AsNoTracking() 
                        select 
                            this.mapper.Map<DTOAPI_RoutePages>(x) ).ToList();

            var tree = List.GenerateTree(c => c.id,  c => c.parentId, (DTOAPI_RoutePages c, IEnumerable<DTOAPI_RoutePages> val)  => 
            {
                c.children = val.ToArray();
            }, root_id: null).ToArray();

            return new DTOAPIRes_RoutePages { routes = tree};
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DTOAPIRes_RoutePages> GetRoutePage(long Id)
        {
            RoutePages routePage =  this.accesser.Get(Id);
            if (routePage != null)
            {
                var List = (from 
                                x 
                            in 
                                this.accesser.db.RoutePages.AsNoTracking()
                            where 
                                x.HierarchyPath.StartsWith(routePage.HierarchyPath) 
                            select
                                this.mapper.Map<DTOAPI_RoutePages>(x)
                            ).ToList();

                var tree = List.GenerateTree(c => c.id, c => c.parentId, (DTOAPI_RoutePages c, IEnumerable<DTOAPI_RoutePages> val) =>
                {
                    c.children = val.ToArray();
                }, root_id: null).ToArray();

                return new DTOAPIRes_RoutePages { routes = tree };
            }
            else 
            {
                throw new NullReferenceException($"页面路由 ID{Id} 不存在");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<dynamic> AddRoutePage(DTOAPI_RoutePages item)
        {

            Int64 result = 0;
            Int64 NewID;
            DTOIn_PageRoute obj = this.mapper.Map<DTOIn_PageRoute>(item);

            if (item.parentId != null)
            {
                var parentPageRoutes = this.accesser.Get(item.parentId.Value);

                if (parentPageRoutes == null)
                {
                    //父类不存在
                    return -1;
                }

                NewID = this.IDGenerator.GetNewID<RoutePages>();
                obj.Id = NewID;
                obj.HierarchyPath = TreeHelper.GenerateHierarchyPath( parentPageRoutes.HierarchyPath ,NewID);
            }
            else 
            {
                NewID = this.IDGenerator.GetNewID<RoutePages>();
                obj.Id = NewID;
                obj.HierarchyPath = TreeHelper.GenerateHierarchyPath("", NewID);
            }

            await this.publishEndpoint.Publish(new AddPageRouteCommand
            {
                data = obj
            }) ;

            result = NewID;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<dynamic> AddRoutePages(DTOAPI_RoutePages item)
        {
            IList<DTOIn_PageRoute> list = new List<DTOIn_PageRoute>();

            Int64 result = 0;

            TreeItem<long> data = new TreeItem<long>();

            item.Foreach(x => x.children, (parent, x) =>
            {
                long NewID = this.IDGenerator.GetNewID<RoutePages>();

                DTOIn_PageRoute obj = this.mapper.Map<DTOIn_PageRoute>(x);

                if (parent == null)
                {
                    if (item == x)
                    {
                        result = NewID;
                        // 确保 根节点 HierarchyPath 的正确性
                        if (item.parentId != null)
                        {
                            RoutePages routepage = this.accesser.Get(item.parentId.Value);
                            if (routepage != null)
                            {
                                item.hierarchyPath = TreeHelper.GenerateHierarchyPath(routepage.HierarchyPath, NewID);

                                obj.HierarchyPath = item.hierarchyPath;
                            }
                        }
                    }

                    if (item == x)
                    {
                        result = NewID;
                    }
                }
                else
                {
                    obj.HierarchyPath = TreeHelper.GenerateHierarchyPath(parent.hierarchyPath, NewID);
                }

                list.Add(obj);
            });
            await this.publishEndpoint.Publish(new AddPageRoutesCommand
            {
                routes = list
            });

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routepage"></param>
        /// <returns></returns>
        public async Task<dynamic> UpdateRoutePage(DTOAPI_RoutePages routepage)
        {
            var cmd = new UpdatePageRouteCommand
            {
                data = this.mapper.Map<DTOIn_PageRoute>(routepage)
            };
            
            await this.publishEndpoint.Publish(cmd);
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<dynamic> DeleteRoutePage(long id)
        {
            await this.publishEndpoint.Publish(new DeletePageRouteCommand
            {
                id = id
            }) ;
            return 1;
            //return this.accesser.Delete(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IList<DTOAPI_RoutePages>> GetRoutePageTreeByRoles(IList<long> ids)
        {
            var id = ids.First();
            var role = (from 
                x 
            in 
                this.accesser.db.Roles.Where(x => x.Id == id).Include(p => p.RouteRoles).ThenInclude(p => p.routePage)
            select 
                x).FirstOrDefault();
            
            if (role != null)
            {
                var ret_data = GenPageRouteTree(role);
                return ret_data;
            }
            else 
            {
                throw new NullReferenceException("role is null...");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<dynamic> GetRoutePageByRoles(IList<long> ids) 
        {
            var id = ids.First();
            var role = (from
                            x
                        in
                            this.accesser.db.Roles.Where(x => x.Id == id)
                                                  .Include(p => p.RouteRoles)
                                                  .ThenInclude(p => p.routePage)
                        select
                            x ).FirstOrDefault();


            if (role != null)
            {
                return role.RouteRoles.Select(x => this.mapper.Map<DTOAPI_RoutePages>(x)) .ToArray();
            }
            else
            {
                throw new NullReferenceException("role is null...");
            }
        }

        #region private 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private IList<DTOAPI_RoutePages> GenPageRouteTree(Role role)
        {
            IList<DTOAPI_RoutePages> routes = new List<DTOAPI_RoutePages>();
            if (role.RouteRoles != null && role.RouteRoles.Count > 0)
            {
                routes = role.RouteRoles.Select(x =>
                {
                    var routePage      = this.mapper.Map<DTOAPI_RoutePages>(x);
                    return routePage;
                } ).GenerateTree(x => x.id, x => x.parentId, (n, children) =>
                {
                    n.children = (children.Count() > 0 ? children.ToList() : null);
                }, null).ToList();
            }

            return routes;
        }
        #endregion
    }
}
