using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBAccessCoreDLL.EF.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class RoutePage : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Int64 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Nullable<Int64> ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Component { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string RouteName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual RoutePageMeta Meta { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<RoutePageRole> RoutePageRoles { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RoutePageEFConfig : IEntityTypeConfiguration<RoutePage>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<RoutePage> builder)
        {
            var tableBuilder = builder.ToTable("RoutePage")
                   .OwnsOne<RoutePageMeta>(p => p.Meta, c =>
            {
                c.ToTable("RoutePageMeta");
            });

            tableBuilder.Property(x => x.ParentId).HasDefaultValue(null);


            tableBuilder.SetupBaseEntity<RoutePage>();
        }

    }
}
