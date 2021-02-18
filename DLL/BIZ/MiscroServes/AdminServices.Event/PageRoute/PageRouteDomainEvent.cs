using AdminServices.Command.PageRouteRole;
using AdminServices.Command.Role;
using AutoMapper;
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

        readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_IDGenerator"></param>
        /// <param name="_roleAccesser"></param>
        /// <param name="_mapper"></param>
        public PageRouteDomainEvent(IIDGenerator _IDGenerator, IRoutePageAccesser _routePageAccesser, IMapper _mapper)
            : base()
        {
            this.accesser       = _routePageAccesser;
            this.IDGenerator    = _IDGenerator;
            this.mapper = mapper;
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
                GroupName         = x.GroupName  ??   "",
                Platform      = x.Platform ?? "",
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
            var msg = context.Message.data ;
            
            RoutePages route = this.accesser.Get(msg.Id);
            
            if (route == null) 
            {
                throw new NullReferenceException("Role is null");
            }

            route.RouteName = msg.RouteName ?? "";
            route.Path       = msg.Path ?? "";
            route.Component  = msg.Component ?? "";
            route.NoCache    = msg.NoCache;
            route.Affix      = msg.Affix;
            route.GroupName = msg.GroupName ?? "";
            route.Platform = msg.Platform ?? "";
            route.ActiveMenu = msg.ActiveMenu ?? "";
            route.AlwaysShow = msg.AlwaysShow;
            route.Hidden     = msg.Hidden;
            route.Icon       = msg.Icon;
            route.Title      = msg.Title;

            int effectRow = this.accesser.Update(route);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<AddPageRouteCommand> context)
        {
            DTOIn_PageRoute data = context.Message.data;

            var pageRoute = new RoutePages
            {
                Id              = data.Id,
                ParentId        = data.ParentId,
                RouteName       = data.RouteName ?? "",
                HierarchyPath   = data.HierarchyPath,
                Path            = data.Path ?? "",
                Component       = data.Component,
                NoCache         = data.NoCache,
                Affix           = data.Affix,
                ActiveMenu      = data.ActiveMenu,
                AlwaysShow      = data.AlwaysShow,
                Hidden          = data.Hidden,
                Icon            = data.Icon,
                Title           = data.Title
            };

            this.accesser.Add(pageRoute);
        }
    }
}
