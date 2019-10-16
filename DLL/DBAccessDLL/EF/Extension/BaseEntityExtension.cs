using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessDLL.EF.Entity
{
    /// <summary>
    /// 
    /// </summary>
    static public class BaseEntityExtension
    {
        /// <summary>
        /// Fill Default Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        static public void Fill<T>(this T target) where T : BaseEntity
        {
            target.Qing_CreateTime  = DateTime.Now  ;
            target.Qing_UpdateTime  = DateTime.Now  ;
            target.Qing_DeleteTime  = null          ;
            target.Qing_IsDelete    = false         ;
            target.Qing_Version     = 0             ;
            target.Qing_Sequence    = 0             ;
        }

    }
}
