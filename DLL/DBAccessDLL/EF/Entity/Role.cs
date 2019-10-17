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
        /// 关联的AccountRoles数据
        /// </summary>
        public ICollection<AccountRole> AccountRoles { get; set; }

        /// <summary>
        /// 关联的
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

            tableBuilder.HasMany<AccountRole>   (  p => p.AccountRoles  ).WithOne(c => c.role).HasForeignKey(c => c.RoleId);
            tableBuilder.HasMany<RoutePageRole> (  p => p.RouteRoles    ).WithOne(c => c.role).HasForeignKey(c => c.RoleId);
#if DEBUG
            #region Default Database
            
            Role[] default_roles = {
                                     new Role { Id = 1, Descrption = "系统管理员", Name = "admin"  } ,
                                     new Role { Id = 2, Descrption = "系统运维员", Name = "editor" } ,
                                     new Role { Id = 3, Descrption = "访客",       Name = "guest"  } 
                                   };

            tableBuilder.HasData(default_roles);
            #endregion
#endif
        }
    }
}
