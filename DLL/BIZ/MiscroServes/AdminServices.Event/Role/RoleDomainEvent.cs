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
namespace AdminServices.Event.Role
{
    /// <summary>
    /// Role领域消费
    /// </summary>
    public class RoleDomainEvent : 
        IConsumer<AddRoleCommand>,
        IConsumer<UpdateRoleCommand>,
        IConsumer<DeleteRoleCommand>
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
        /// <param name="_roleAccesser"></param>
        /// <param name="_mapper"></param>
        public RoleDomainEvent(IIDGenerator _IDGenerator, IRoleAccesser _roleAccesser)
            : base()
        {
            this.accesser = _roleAccesser;
            this.IDGenerator = _IDGenerator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<AddRoleCommand> context)
        {
            var msg = context.Message;

            var routes = msg.routes.Select(x => new RoutePageRole
            {
                Id = IDGenerator.GetNewID<RoutePageRole>(),
                RoleId = msg.key,
                RoutePageId = x.PageRouteID
            }).ToArray();

            Role_Alias role = new Role_Alias
            {
                Id = msg.key,
                RoleName = msg.name,
                DisplayName = msg.name,
                Descrption = msg.description,
                RouteRoles = routes
            };

            this.accesser.Add(role);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume( ConsumeContext<UpdateRoleCommand> context )
        {
            var msg = context.Message;
            var role = this.accesser.Get(msg.key);

            if (role != null)
            {
                role.Descrption = msg.description;
                role.DisplayName = msg.displayName;
                role.RoleName = msg.name;
                role.Descrption = msg.description;

                var routes = msg.routes.Select(x => new RoutePageRole
                {
                    Id      = this.IDGenerator.GetNewID<RoutePageRole>(), 
                    RoleId  = role.Id,
                    RoutePageId = x.PageRouteID
                }).ToArray();

                role.RouteRoles = routes;

                var oldRoute = (from x in this.accesser.db.RoutePageRoles where x.RoleId == msg.key select x);
                var count = oldRoute.Count();
                if (count > 0)
                {
                   var deleteRoutes = oldRoute.ToArray();
                   this.accesser.db.RoutePageRoles.RemoveRange(deleteRoutes);
                }
                this.accesser.db.RoutePageRoles.AddRange(routes);
                this.accesser.Update(role);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<DeleteRoleCommand> context)
        {
            int rowEffect = this.accesser.Delete(context.Message.key);
        }
    }
}
