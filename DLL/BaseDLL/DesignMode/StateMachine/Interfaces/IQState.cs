using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDLL.DesignMode.StateMachine.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IQState
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Handle(IQContext context);

    }
}
