using BaseDLL.Helper;
using Bogus.DataSets;
using BusinessAdminDLL.Base;
using BusinessAdminDLL.DTOModel.API.Roles;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EF.Context;
using DBAccessCoreDLL.EF.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RoutePage_Alias = DBAccessCoreDLL.EF.Entity.RoutePage;
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

        readonly IRoutePageAccesser services;

        /// <summary>
        /// 
        /// </summary>
        readonly CoreContextDIP db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="_db"></param>
        /// <param name="_services"></param>
        public RoutePageBizServices(IIDGenerator _IDGenerator, CoreContextDIP _db, IRoutePageAccesser _services)
            : base()
        {
            this.db = _db;
            this.IDGenerator = _IDGenerator;
            this.services = _services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TreeItem<RoutePage_Alias>[] GetRoutePages()
        {
            var List = (from x in this.db.RoutePages select x).ToList();

            var tree = List.GenerateTree(c => c.Id, c => c.ParentId,root_id: null).ToArray();
            return tree;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int AddRoutePages() 
        {
            int effectCount = 0;
            return effectCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TreeItem<RoutePage_Alias> GetRoutePage(long Id)
        {
            var List = (from x in this.db.RoutePages select x ).ToList();

            TreeItem<RoutePage_Alias> tree = new TreeItem<RoutePage_Alias>();
            tree.node = List.Where(x => x.Id == Id).FirstOrDefault();
            tree.children = List.GenerateTree(c => c.Id, c => c.ParentId, Id, 1).ToArray();
            
            tree.deep = 0;
            return tree;//this.db.RoutePages.Find(Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public dynamic AddRoutePage(DTOAPI_RoutePages item)
        {
            //var parent_RoutePage = this.db.RoutePages.Find(routepage.parentId);

            IList<RoutePage_Alias> list = new List<RoutePage_Alias>();
            item.Foreach(x => x.children, (parent,x) =>
              {
                  long NewID = this.IDGenerator.GetNewID<RoutePage_Alias>();

                  list.Add(new RoutePage_Alias
                  {
                      Id = NewID,
                      ParentId = x.parentId,
                      RouteName = x.name ?? "",
                      HierarchyPath = TreeHelper.GenerateHierarchyPath(parent != null ? parent.hierarchyPath : "", NewID),
                      Path = x.path ?? "",
                      Component = x.component,
                      NoCache = x.meta.noCache,
                      Affix = x.meta.affix,
                      ActiveMenu = x.meta.activeMenu,
                      AlwaysShow = x.meta.alwaysShow,
                      Hidden = x.meta.hidden,
                      Icon = x.meta.icon ,
                      Title = x.meta.title
                  });
              });
            return this.services.Add(list);
            //return this.db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routepage"></param>
        /// <returns></returns>
        public dynamic UpdateRoutePage(DTOAPI_RoutePages routepage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public dynamic DeleteRoutePage(long id)
        {
            throw new NotImplementedException();
        }
    }
}
