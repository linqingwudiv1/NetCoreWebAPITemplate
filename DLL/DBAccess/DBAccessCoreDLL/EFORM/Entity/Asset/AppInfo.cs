using Bogus;
using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DBAccessCoreDLL.Entity.Asset
{
    /// <summary>
    /// 
    /// </summary>
    public class AppInfo : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string AppName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string AppVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(false)]
        public bool bLatest { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(false)]
        public bool bForceUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string url { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class AppInfoEFConfig : IEntityTypeConfiguration<AppInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<AppInfo> builder)
        {
            var tableBuilder = builder.ToTable("AppInfo");
            tableBuilder.HasIndex( x => new { x.AppName, x.AppVersion } ).IsUnique(true);

            builder.SetupBaseEntity();
        }
    }
}
