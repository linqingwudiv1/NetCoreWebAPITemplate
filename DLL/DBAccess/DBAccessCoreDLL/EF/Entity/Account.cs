using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBAccessCoreDLL.EF.Entity
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class Account : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Account() 
        {
            this.Sex = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Int64 Id { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 通行证
        /// </summary>
        public string Passport { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 现实名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }

    /// <summary>
    /// Entit Config
    /// </summary>
    public class AccountEFConfig : IEntityTypeConfiguration<Account>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            #region 水平拆分处理处

            EntityTypeBuilder<Account> tableBuilder = builder.ToTable("Account");

            tableBuilder.HasMany<AccountRole>( p => p.AccountRoles )
                        .WithOne( c => c.account )
                        .HasForeignKey( c => c.AccountId );

            #endregion

            builder.SetupBaseEntity();
        }
    }
}
