using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessBaseDLL.Accesser
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Entity">数据实体类型</typeparam>
    /// <typeparam name="Key">主键类型</typeparam>
    public interface IAccesser<Entity, Key>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Entity Get(Key key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        IList<Entity> Get(IList<Key> keys);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiy"></param>
        /// <returns></returns>
        int Add(Entity newEntiy);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiys"></param>
        /// <returns></returns>
        int Add(IList<Entity> newEntiys);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int Delete(Key key);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Key"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        int Delete(IList<Key> keys);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifyEntiy"></param>
        /// <returns></returns>
        int Update(Entity modifyEntiy);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifyEntiys"></param>
        /// <returns></returns>
        int Update(IList<Entity> modifyEntiys);
    }
}
