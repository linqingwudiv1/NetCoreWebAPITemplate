using BaseDLL.Helper;
using DBAccessDLL.EF.Context;
using System;

namespace BaseDLL
{
    public class CoreHelper : ICoreHelper
    {
        private readonly ExamContext db ;
        private readonly Guid guid;
        /// <summary>
        /// 
        /// </summary>
        public CoreHelper() 
        {
            guid = Guid.NewGuid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string HelloAutofac()
        {
            return $"HelloAotufac : {guid}";
        }
    }
}
