using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessBaseDLL.IDGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIDGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <returns></returns>
        Int64 GetNewID<Entity>() where Entity : new();

    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class AbsIDGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <returns></returns>
        public virtual string GetKey<Entity>() where Entity : new()
        {
            return "IDGeneration_" + typeof(Entity).FullName; 
        }
    }
}
