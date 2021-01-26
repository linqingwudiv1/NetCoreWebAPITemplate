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
using Role_Alias = DBAccessCoreDLL.EFORM.Entity.Role;
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
            var message = context.Message;

            this.accesser.db(message);
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<DeletePageRouteCommand> context)
        {
            this.accesser.Delete(context.Message.id);
            return;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<UpdatePageRouteCommand> context)
        {
            throw new NotImplementedException();
        }

    }
}
