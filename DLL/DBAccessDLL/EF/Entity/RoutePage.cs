using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBAccessDLL.EF.Entity
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
        public Int64 ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RoutePageMeta Meta { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<RoutePageRole> RoutePageRoles { get; set; }
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
            builder.ToTable("RoutePage").OwnsOne<RoutePageMeta>(p => p.Meta, c =>
            {
                c.ToTable("RoutePageMeta");
            });

            builder.SetupBaseEntity<RoutePage>();
        }
    }
}
