using BaseDLL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class mytestservice : itestservice
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICoreHelper coreHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_coreHelper"></param>
        public mytestservice( ICoreHelper _coreHelper) 
        {
            coreHelper = _coreHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        public string test()
        {
            return $"the app callback of dependency injection to mytestservice : {coreHelper.HelloAutofac()}"; 
        }
    }
}
