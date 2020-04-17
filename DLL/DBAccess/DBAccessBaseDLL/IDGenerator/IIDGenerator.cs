using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessBaseDLL.IDGenerator
{
    interface IIDGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Int64 GetNewID<T>();
    }
}
