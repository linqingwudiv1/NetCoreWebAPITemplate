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
    public class ForumTopicDomain :
        IConsumer<AddForumTopicCommand>,
        IConsumer<UpdateForumTopicCommand>,
        IConsumer<DeleteForumTopicCommand>
    {

        readonly CoreContextDIP db;
        readonly IForumTopicAccesser accesser;
        /// <summary>
        /// 
        /// </summary>
        public ForumTopicDomain(CoreContextDIP _db, IForumTopicAccesser _accesser) 
        {
            this.db = _db;
            this.accesser = _accesser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<AddForumTopicCommand> context)
        {
            var model = context.Message;
            ForumTopic newTopic = new ForumTopic();
            newTopic.Id = model.Id;
            this.accesser.Add(newTopic);
            // throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<UpdateForumTopicCommand> context)
        {
            // throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<DeleteForumTopicCommand> context)
        {
            // throw new NotImplementedException();
        }
    }
}
