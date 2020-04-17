using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessBaseDLL.EF.Entity
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
            target.Q_CreateTime  = DateTime.Now      ;
            target.Q_UpdateTime  = DateTime.Now      ;
            target.Q_DeleteTime  = DateTime.MinValue ;
            target.Q_IsDelete    = false             ;
            target.Q_Version     = 0                 ;
            target.Q_Sequence    = 0                 ;
        }

    }
}
