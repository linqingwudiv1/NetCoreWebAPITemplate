using DBAccessCoreDLL.EFORM.Context;
using System.Configuration;

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
            //string connstr = ConfigurationManager.ConnectionStrings[""].ConnectionString;
            return new CoreContext(); 
        }
    }
}
