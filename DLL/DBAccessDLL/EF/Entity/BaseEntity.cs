using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DBAccessDLL.EF.Entity
{
    public class BaseEntity
    {
        /// <summary>
        /// Soft Delete 软删除
        /// </summary>
        [Required]
        [Display(Description = "Soft Delete 软删除")]
        [DefaultValue(false)]
        public bool Qing_IsDelete { get; set; }

        /// <summary>
        /// Optimistic locking 乐观锁字段
        /// </summary>
        [Required]
        [DefaultValue(0)]
        [Display(Description = "Optimistic locking 乐观锁字段")]
        public Int64 Qing_Version { get; set; }
    }
}
