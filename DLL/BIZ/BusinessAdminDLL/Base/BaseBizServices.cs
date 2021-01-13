﻿using DBAccessCoreDLL.EF.Context;

namespace BusinessAdminDLL.Base
{
    /// <summary>
    /// Biz Core 基础
    /// </summary>
    public abstract class BaseBizServices : IBaseBizServices
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseBizServices() 
        {
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CoreContext GetCoreDB() 
        {
            return new CoreContext(); 
        }
    }
}
