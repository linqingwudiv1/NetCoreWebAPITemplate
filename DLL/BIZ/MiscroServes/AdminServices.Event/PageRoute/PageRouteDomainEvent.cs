using AdminServices.Command.PageRouteRole;
using AdminServices.Command.Role;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using DBAccessCoreDLL.EFORM.Entity;
using MassTransit;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AdminServices.Event.PageRoute
{
    /// <summary>
    /// Role领域消费
    /// </summary>
    public class PageRouteDomainEvent :
                IConsumer<AddPageRoutesCommand>,
                IConsumer<UpdatePageRouteCommand>,
                IConsumer<DeletePageRouteCommand>,
                IConsumer<AddPageRouteCommand>        
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
        /// <param name="_roleAccesser"></param>
        /// <param name="_mapper"></param>
        public PageRouteDomainEvent(IIDGenerator _IDGenerator, IRoutePageAccesser _routePageAccesser)
            : base()
        {
            this.accesser       = _routePageAccesser;
            this.IDGenerator    = _IDGenerator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<AddPageRoutesCommand> context)
        {
            var msg = context.Message;
            var pageRoutes = msg.routes.Select( x => new RoutePages
            {
                Id       = x.Id                 ,
                ParentId = x.ParentId           ,
                RouteName = x.RouteName ?? ""   ,
                HierarchyPath = x.HierarchyPath ,
                Path = x.Path ?? ""             ,
                Component = x.Component         ,
                NoCache = x.NoCache             ,
                Affix = x.Affix                 ,
                ActiveMenu = x.ActiveMenu       ,
                AlwaysShow = x.AlwaysShow       ,
                Hidden  = x.Hidden              ,
                Icon    = x.Icon                ,
                Title   = x.Title               
            }).ToArray();


            this.accesser.Add(pageRoutes);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<DeletePageRouteCommand> context)
        {
            this.accesser.Delete(context.Message.id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<UpdatePageRouteCommand> context)
        {
            var msg = context.Message;
            
            RoutePages role = this.accesser.Get(msg.Id);

            role.Id = msg.Id;
            role.RouteName = msg.RouteName ?? "";
            role.Path       = msg.Path ?? "";
            role.Component  = msg.Component;
            role.NoCache    = msg.NoCache;
            role.Affix      = msg.Affix;
            role.ActiveMenu = msg.ActiveMenu;
            role.AlwaysShow = msg.AlwaysShow;
            role.Hidden     = msg.Hidden;
            role.Icon       = msg.Icon;
            role.Title      = msg.Title;

            this.accesser.Update(role);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<AddPageRouteCommand> context)
        {
            AddPageRouteCommand msg = context.Message;

            var pageRoute = new RoutePages
            {
                Id              = msg.data.Id,
                ParentId        = msg.data.ParentId,
                RouteName       = msg.data.RouteName ?? "",
                HierarchyPath   = msg.data.HierarchyPath,
                Path            = msg.data.Path ?? "",
                Component       = msg.data.Component,
                NoCache         = msg.data.NoCache,
                Affix           = msg.data.Affix,
                ActiveMenu      = msg.data.ActiveMenu,
                AlwaysShow      = msg.data.AlwaysShow,
                Hidden          = msg.data.Hidden,
                Icon            = msg.data.Icon,
                Title           = msg.data.Title
            };

            this.accesser.Add(pageRoute);
        }
    }
}
