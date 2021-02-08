using AdminServices.Command.PageRouteRole;
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
    public static class RoutePageBizServicesExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        static public IList<DTOAPI_RoutePages> GenPageRouteTree(this Role role)
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
                    n.children = (children.Count() > 0 ? children.ToList() : null);
                }, null).ToList();
            }

            return routes;
        }
    }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="_accesser"></param>
        /// <param name="_publishEndpoint"></param>
        public RoutePageBizServices(IIDGenerator _IDGenerator, 
                                    IRoutePageAccesser _accesser,
                                    IPublishEndpoint _publishEndpoint)
            : base()
        {
            this.IDGenerator = _IDGenerator;
            this.accesser = _accesser;
            this.publishEndpoint = _publishEndpoint;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<DTOAPIRes_RoutePages> GetRoutePages()
        {
            var List = (from x in this.accesser.db.RoutePages select new DTOAPI_RoutePages
                {
                    id = x.Id,
                    parentId = x.ParentId,
                    hierarchyPath = x.HierarchyPath,
                    component = x.Component,
                    name = x.RouteName,
                    path = x.Path,
                    platform = x.Platform,
                    groupName = x.GroupName,
                    redirect = x.Redirect,
                    //children = new List<DTOAPI_RoutePages>(),
                    meta = new DTOAPI_RoutePagesMeta
                    {
                        title = x.Title,
                        activeMenu = x.ActiveMenu,
                        affix = x.Affix,
                        alwaysShow = x.AlwaysShow,
                        hidden = x.Hidden,
                        icon = x.Icon,
                        noCache = x.NoCache
                    }
                }).ToList();

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
                var List = (from x in this.accesser.db.RoutePages 
                            where 
                                x.HierarchyPath.StartsWith(routePage.HierarchyPath) 
                            select
                            new DTOAPI_RoutePages
                            {
                                id = x.Id,
                                parentId = x.ParentId,
                                hierarchyPath = x.HierarchyPath,
                                component = x.Component,
                                name = x.RouteName,
                                path = x.Path,
                                redirect = x.Redirect,
                                platform = x.Platform,
                                groupName = x.GroupName,
                                //children = new List<DTOAPI_RoutePages>(),
                                meta = new DTOAPI_RoutePagesMeta
                                {
                                    title = x.Title,
                                    activeMenu = x.ActiveMenu,
                                    affix = x.Affix,
                                    alwaysShow = x.AlwaysShow,
                                    hidden = x.Hidden,
                                    icon = x.Icon,
                                    noCache = x.NoCache
                                }
                            }).ToList();

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

            // routePage.
            //var List = ( from x in this.accesser.db.RoutePages select x ).ToList();
            //
            //TreeItem<RoutePages> tree = new TreeItem<RoutePages>();
            //tree.node = List.Where(x => x.Id == Id).FirstOrDefault();
            //tree.children = List.GenerateTree(c => c.Id, c => c.ParentId, Id, 1).ToArray();
            //
            //tree.deep = 0;
            //return tree;
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
            DTOIn_PageRoute obj = new DTOIn_PageRoute
            {
                ParentId = item.parentId,
                RouteName = item.name ?? "",
                //HierarchyPath = item.hierarchyPath,
                Path = item.path ?? "",
                Redirect = item.redirect  ?? null ,
                Component = item.component ?? "",
                NoCache = item.meta.noCache,
                Affix = item.meta.affix,
                ActiveMenu = item.meta.activeMenu ?? "",
                AlwaysShow = item.meta.alwaysShow,
                Hidden = item.meta.hidden,
                Icon = item.meta.icon ?? "",
                Title = item.meta.title ?? ""
            };

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
        /// <param name="routepage"></param>
        /// <returns></returns>
        public async Task<dynamic> AddRoutePages(DTOAPI_RoutePages item)
        {
            IList<DTOIn_PageRoute> list = new List<DTOIn_PageRoute>();

            Int64 result = 0;

            TreeItem<long> data = new TreeItem<long>();

            item.Foreach(x => x.children, (parent, x) =>
            {
                long NewID = this.IDGenerator.GetNewID<RoutePages>();

                DTOIn_PageRoute obj = new DTOIn_PageRoute
                {
                    Id = NewID,
                    ParentId = x.parentId,
                    RouteName = x.name ?? "",
                    Path = x.path ?? "",
                    Redirect = x.redirect ,
                    Component = x.component ?? "",
                    Platform = x.platform ?? "",
                    GroupName   = x.groupName ?? "",
                    NoCache = x.meta.noCache,
                    Affix = x.meta.affix,
                    ActiveMenu = x.meta.activeMenu ?? "",
                    AlwaysShow = x.meta.alwaysShow,
                    Hidden = x.meta.hidden,
                    Icon = x.meta.icon      ?? "",
                    Title = x.meta.title    ?? ""
                };

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
            await this.publishEndpoint.Publish(new UpdatePageRouteCommand
            {
                Id          = routepage.id              ,
                RouteName   = routepage.name ?? ""      ,
                Path        = routepage.path ?? ""      ,
                Component   = routepage.component       ,
                GroupName   = routepage.groupName           ,
                Platform    = routepage.platform        ,
                NoCache     = routepage.meta.noCache    ,
                Affix       = routepage.meta.affix      ,
                ActiveMenu  = routepage.meta.activeMenu ,
                AlwaysShow  = routepage.meta.alwaysShow ,
                Hidden      = routepage.meta.hidden     ,
                Icon        = routepage.meta.icon       ,
                Title       = routepage.meta.title
            });

            return 1;
            //throw new NotImplementedException();
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
                var ret_data = role.GenPageRouteTree();
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
                this.accesser.db.Roles.Where(x => x.Id == id).Include(p => p.RouteRoles).ThenInclude(p => p.routePage)
            select
                x ).FirstOrDefault();


            if (role != null)
            {
                return role.RouteRoles.Select(x => new DTOAPI_RoutePages 
                {
                    id            = x.routePage.Id,
                    parentId      = x.routePage.ParentId,
                    hierarchyPath = x.routePage.HierarchyPath,
                    component     = x.routePage.Component,
                    name          = x.routePage.RouteName,
                    path          = x.routePage.Path,
                    redirect      = x.routePage.Redirect,
                    groupName     = x.routePage.GroupName,
                    platform      = x.routePage.Platform,
                    meta = new DTOAPI_RoutePagesMeta
                    {
                        title       = x.routePage.Title,
                        activeMenu  = x.routePage.ActiveMenu,
                        affix       = x.routePage.Affix,
                        alwaysShow  = x.routePage.AlwaysShow,
                        hidden      = x.routePage.Hidden,
                        icon        = x.routePage.Icon,
                        noCache     = x.routePage.NoCache
                    }
                }) .ToArray();
            }
            else
            {
                throw new NullReferenceException("role is null...");
            }
        }
    }
}
