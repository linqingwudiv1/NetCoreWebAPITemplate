using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DBAccessDLL.EF.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class BizAccountLog
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Int64 Id { get; set; }

        /// <summary>
        /// 关联Account ID
        /// </summary>
        public Int64 AccountId { get; set; }

        /// <summary>
        /// 机器名
        /// </summary>
        [Required]
        public string MachineName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Level { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// IP地址/或用户ID
        /// </summary>
        [Required]
        public string Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BizAccountLogEFConfig : IEntityTypeConfiguration<BizAccountLog>
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<BizAccountLog> builder)
        {
            var tableBuilder = builder.ToTable("BizAccountLog");

            tableBuilder.Property(x => x.Id).HasIdentityOptions(1, 1);
            tableBuilder.Property(x => x.CreateTime).HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
