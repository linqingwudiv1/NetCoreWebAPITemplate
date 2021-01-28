using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBAccessCoreDLL.EFORM.Entity
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
        public String RoleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Organization { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Descrption { get; set; }

        /// <summary>
        /// 关联的AccountRoles数据
        /// </summary>
        public virtual ICollection<AccountRole> AccountRoles { get; set; }

        /// <summary>
        /// 关联的
        /// </summary>
        public virtual ICollection<RoutePageRole> RouteRoles { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public Nullable<Int64> ParentId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual Role Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Role> Children { get; set; }
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

            tableBuilder.Property( c => c.Organization ).HasDefaultValue("Default");

            tableBuilder.HasMany<AccountRole>   (  p => p.AccountRoles  )
                        .WithOne(c => c.role)
                        .HasForeignKey(c => c.RoleId);

            tableBuilder.HasMany<RoutePageRole> (  p => p.RouteRoles    )
                        .WithOne(c => c.role)
                        .HasForeignKey(c => c.RoleId);

            tableBuilder.HasOne<Role> ( p => p.Parent   )
                        .WithMany     ( c => c.Children )
                        .HasForeignKey( c => c.ParentId );

#if DEBUG
            #region Default Database

            Role[] default_roles = {
                                     new Role { Id = 1, ParentId = null, Descrption = "系统管理员", DisplayName = "系统管理员", RoleName = "admin"      } ,
                                     new Role { Id = 2, ParentId = null, Descrption = "开发程序员", DisplayName = "开发程序员", RoleName = "developer"  } ,
                                     new Role { Id = 3, ParentId = null, Descrption = "编辑人员"  , DisplayName = "编辑人员"  , RoleName = "editor"     } ,
                                     new Role { Id = 4, ParentId = null, Descrption = "访客"      , DisplayName = "访客"      , RoleName = "guest"      } ,
                                   };

            tableBuilder.HasData(default_roles);

            #endregion
#endif
        }
    }
}
