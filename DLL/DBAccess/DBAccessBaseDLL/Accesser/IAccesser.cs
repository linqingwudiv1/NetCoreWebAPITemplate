using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessBaseDLL.Accesser
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccesser<Entity>
    {
        /// <summary>
        /// C
        /// </summary>
        /// <returns></returns>
        Entity Get<Key>(Key key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiy"></param>
        /// <returns></returns>
        int Add(Entity newEntiy);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int Delete<Key>(Key key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deleteEntiy"></param>
        /// <returns></returns>
        int Update(Entity deleteEntiy);


    }
}
