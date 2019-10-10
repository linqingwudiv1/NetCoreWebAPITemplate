using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DBAccessDLL.EF.Entity
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class AccountRole : BaseEntity
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
        public Int64 AccountId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Account account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public Int64 RoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Role role { get; set; }
    }

    /// <summary>
    /// Entity Config
    /// </summary>
    public class AccountRoleEFConfig : IEntityTypeConfiguration<AccountRole>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<AccountRole> builder)
        {
            #region 水平拆分处理处

            var tableBuilder = builder.ToTable("AccountRole");
            //tableBuilder.HasOne<Role>( p => p.role).WithOne().HasForeignKey();

           // tableBuilder.HasOne<Account>(p => p.account).WithMany().HasForeignKey();
           // tableBuilder.HasOne<Account>(p => p.account).WithMany().HasForeignKey (p => p.RoleId);
           //
           // tableBuilder.HasOne


            #endregion

            builder.SetupBaseEntity();
        }

    }
}
