using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DBAccessDLL.EF.Entity
{
    /// <summary>
    /// 路由得Role列表
    /// </summary>
    public class RoutePageRole : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Int64 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Int64 RoutePageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual RoutePage routePage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Int64 RoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Role role { get; set; }
    }

    /// <summary>
    /// Entity Config
    /// </summary>
    public class RoutePageRoleEFConfig : IEntityTypeConfiguration<RoutePageRole>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<RoutePageRole> builder)
        {
            #region 水平拆分处理处

            EntityTypeBuilder<RoutePageRole> tableBuilder = builder.ToTable("RoutePageRole");

            tableBuilder.HasOne<RoutePage>(p => p.routePage).WithMany(c => c.RoutePageRoles).HasForeignKey(c => c.RoutePageId);
            tableBuilder.HasOne<Role>(p => p.role).WithMany(c => c.RouteRoles).HasForeignKey(c => c.RoleId);

            #endregion

            builder.SetupBaseEntity();
        }

    }
}
