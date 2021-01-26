using AdminServices.Command.PageRouteRole;
using AdminServices.Command.Role;
using BaseDLL.Helper;
using BusinessAdminDLL.Base;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using MassTransit;
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
        public TreeItem<RoutePages>[] GetRoutePages()
        {
            var List = (from x in this.accesser.db.RoutePages select x).ToList();

            var tree = List.GenerateTree(c => c.Id, c => c.ParentId,root_id: null).ToArray();

            return tree;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TreeItem<RoutePages> GetRoutePage(long Id)
        {
            var List = ( from x in this.accesser.db.RoutePages select x ).ToList();

            TreeItem<RoutePages> tree = new TreeItem<RoutePages>();
            tree.node = List.Where(x => x.Id == Id).FirstOrDefault();
            tree.children = List.GenerateTree(c => c.Id, c => c.ParentId, Id, 1).ToArray();
            
            tree.deep = 0;
            return tree;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> AddRoutePage(DTOAPI_RoutePages item)
        {
            IList<DTOIn_PageRoute> list = new List<DTOIn_PageRoute>();

            Int64 result = 0;

            item.Foreach(x => x.children, (parent,x) =>
            {
                long NewID = this.IDGenerator.GetNewID<RoutePages>();

                DTOIn_PageRoute obj = new DTOIn_PageRoute
                {
                    Id = NewID,
                    ParentId = x.parentId,
                    RouteName = x.name ?? "",
                    Path = x.path ?? "",
                    Component = x.component ?? "",
                    NoCache = x.meta.noCache,
                    Affix = x.meta.affix,
                    ActiveMenu = x.meta.activeMenu ?? "",
                    AlwaysShow = x.meta.alwaysShow,
                    Hidden = x.meta.hidden,
                    Icon = x.meta.icon ?? "",
                    Title = x.meta.title ?? ""
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
                }
                else
                {
                    obj.HierarchyPath = TreeHelper.GenerateHierarchyPath(parent.hierarchyPath , NewID);
                }

                list.Add(obj);

            });
            await this.publishEndpoint.Publish(new AddPageRoutesCommand
            {
                routes = list
            }) ;

            return (int)result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routepage"></param>
        /// <returns></returns>
        public async Task<int> UpdateRoutePage(DTOAPI_RoutePages routepage)
        {
            await this.publishEndpoint.Publish(new UpdatePageRouteCommand
            {
                Id          = routepage.id              ,
                RouteName   = routepage.name ?? ""      ,
                Path        = routepage.path ?? ""      ,
                Component   = routepage.component       ,
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
        public async Task<int> DeleteRoutePage(long id)
        {
            await this.publishEndpoint.Publish(new DeletePageRouteCommand
            {
                id = id
            }) ;
            return 1;
            //return this.accesser.Delete(id);
        }

    }
}
