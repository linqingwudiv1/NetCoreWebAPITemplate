using BaseDLL.DesignMode.StateMachine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDLL.DesignMode.StateMachine
{
    /// <summary>
    /// 状态机上下文
    /// </summary>
    public class QContext : IQContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IQState State { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public void Handle()
        {
            this.State.Handle(this);
        }
    }
}
