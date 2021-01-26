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
using RoutePages_Alias = DBAccessCoreDLL.EFORM.Entity.RoutePages;
namespace AdminServices.Event.PageRoute
{
    /// <summary>
    /// Role领域消费
    /// </summary>
    public class PageRouteDomainEvent :
                IConsumer<AddPageRoutesCommand>,
                IConsumer<UpdatePageRouteCommand>,
                IConsumer<DeletePageRouteCommand>
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

            var pageRoutes = msg.routes.Select( x => new RoutePages_Alias
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

            var role = new RoutePages_Alias
            {
                Id          = msg.Id                ,
                RouteName   = msg.RouteName ?? ""   ,
                Path        = msg.Path      ?? ""   ,
                Component   = msg.Component         ,
                NoCache     = msg.NoCache           ,
                Affix       = msg.Affix             ,
                ActiveMenu  = msg.ActiveMenu        ,
                AlwaysShow  = msg.AlwaysShow        ,
                Hidden      = msg.Hidden            ,
                Icon        = msg.Icon              ,
                Title       = msg.Title
            };

            this.accesser.Update(role);
        }

    }
}
