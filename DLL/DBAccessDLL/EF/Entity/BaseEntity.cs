using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DBAccessDLL.EF.Entity
{
    /// <summary>
    /// 基础实体类/
    /// </summary>
    abstract public class BaseEntity
    {
        /// <summary>
        /// Soft Delete 软删除
        /// </summary>
        public bool Qing_IsDelete { get; set; }

        /// <summary>
        /// 数据版本 Optimistic locking 乐观锁字段
        /// </summary>
        public Int64 Qing_Version { get; set; }

        /// <summary>
        /// 预留/消息队列用:概指当前行数据是第几个消息，方便用于事件回溯扩展(Event Sourcing)
        /// </summary>
        public Int64 Qing_Sequence { get; set; }

        /// <summary>
        /// 数据创建时间
        /// </summary>
        public DateTime Qing_CreateTime { get; set; }

        /// <summary>
        /// 数据最近一次修改时间
        /// </summary>
        public DateTime Qing_UpdateTime { get; set; }

        /// <summary>
        /// 软删除事件
        /// </summary>
        public DateTime Qing_DeleteTime { get; set; }
    }
}
