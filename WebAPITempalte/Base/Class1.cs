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
    public class Class1
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICoreHelper coreHelper;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_coreHelper"></param>
        public Class1(ICoreHelper _coreHelper) 
        {
            //coreHelper = _coreHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string test() 
        {
            return coreHelper.HelloAutofac();
        }
    }
}
