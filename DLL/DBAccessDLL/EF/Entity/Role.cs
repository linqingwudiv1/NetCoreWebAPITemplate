using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DBAccessDLL.EF.Entity
{
    /// <summary>
    /// Account 角色 Account Role
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Int64 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Name { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Descrption { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<AccountRole> AccountRoles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<RoutePageRole> RouteRoles { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RoleEFConfig : IEntityTypeConfiguration<Role>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            var tableBuilder = builder.ToTable("Role").SetupBaseEntity<Role>();
        }

    }
}
