using BaseDLL.Helper;
using Bogus.DataSets;
using BusinessAdminDLL.Base;
using BusinessAdminDLL.DTOModel.API.Roles;
using BusinessAdminDLL.DTOModel.API.Routes;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EF.Entity;
using System;
using System.Collections.Generic;
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
        public RoutePageBizServices(IIDGenerator _IDGenerator, IRoleAccesser RoleAccesser)
            : base()
        {
            this.accesser = RoleAccesser;
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
        public TreeItem<RoutePage_Alias>[] GetRoutePages()
        {
            var List = (from x in this.accesser.db.RoutePages select x).ToList();

            var root = List.GenerateTree(c => c.Id,  c => c.ParentId).ToList();

            return root.ToArray();
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
    }
}
