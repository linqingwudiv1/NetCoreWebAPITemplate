using AdminServices.Command.Forum;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity.Forum;
using DBAccessCoreDLL.Forum;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdminServices.Event.Forum
{
    /// <summary>
    /// 
    /// </summary>
    public class ForumModuleDomain : 
        IConsumer<AddForumModuleCommand>,
        IConsumer<DeleteForumModuleCommand>,
        IConsumer<UpdateForumModuleCommand>
    {

        /// <summary>
        /// 
        /// </summary>
        private readonly CoreContextDIP db;
        
        /// <summary>
        /// 
        /// </summary>
        private readonly IForumModuleAccesser accesser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_db"></param>
        public ForumModuleDomain( CoreContextDIP _db, IForumModuleAccesser _accesser)
        {
            this.db = _db;
            this.accesser = _accesser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<AddForumModuleCommand> context)
        {
            AddForumModuleCommand data = context.Message;

            var entity = new ForumModule();
            entity.Id = data.Id;
            entity.ModuleName = data.ModuleName;

            db.ForumModules.Add(entity);

            db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<DeleteForumModuleCommand> context)
        {
            accesser.Delete(context.Message.Id);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume( ConsumeContext<UpdateForumModuleCommand> context )
        {
            UpdateForumModuleCommand model = context.Message;
            ForumModule entity = this.accesser.Get(model.Id);

            entity.ModuleName = model.ModuleName;
            this.accesser.Update(entity);
        }
    }
}
