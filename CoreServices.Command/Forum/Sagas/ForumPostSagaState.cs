using Automatonymous;
using MassTransit;
using MassTransit.Saga;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminServices.Command.Forum.Sagas
{
   /// <summary>
   /// 
   /// </summary>
    public class ForumPostSagaState : SagaStateMachineInstance
    //InitiatedBy<>
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid CorrelationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CurrentState { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ForumPostStateMachine :
    MassTransitStateMachine<ForumPostSagaState>
    {
        /// <summary>
        /// 
        /// </summary>
        public ForumPostStateMachine()
        {
            InstanceState( x => x.CurrentState );
            // Event( () => AddReplyCmd, x => x.CorrelateById( c => c.Message. Guid.NewGuid() ) );
        }

        /// <summary>
        /// 
        /// </summary>
        public Event<AddForumReplyCommand> AddReplyCmd { get; private set; }
    }
}
