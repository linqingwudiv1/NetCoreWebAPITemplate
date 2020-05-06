using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDLL.DesignMode.StateMachine.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IQContext
    {
        /// <summary>
        /// 
        /// </summary>
        IQState State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        void Handle();
    }
}
