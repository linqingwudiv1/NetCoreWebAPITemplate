using DBAccessCoreDLL.EF.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessCoreDLL.Base
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaseBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        CoreContext GetCoreDB();
    }
}
