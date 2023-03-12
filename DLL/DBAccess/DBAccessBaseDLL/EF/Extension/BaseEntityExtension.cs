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
            target.CreateTime  = DateTime.Now      ;
            target.UpdateTime  = DateTime.Now      ;
            target.DeleteTime  = DateTime.MinValue ;
            target.IsDelete    = false             ;
            target.Version     = 0                 ;
            target.Sequence    = 0                 ;
        }


    }
}
