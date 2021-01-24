using AdminServices.Command.Role;
using DBAccessBaseDLL.IDGenerator;
using DBAccessCoreDLL.Accesser;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public Task Consume(ConsumeContext<AddRoleCommand> context)
        {
            throw new NotImplementedException();
        }

        public Task Consume(ConsumeContext<UpdateRoleCommand> context)
        {
            throw new NotImplementedException();
        }

        public Task Consume(ConsumeContext<DeleteRoleCommand> context)
        {
            throw new NotImplementedException();
        }
    }
}
