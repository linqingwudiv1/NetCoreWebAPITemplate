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
    public class Account : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
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

            builder.ToTable("Account");

            #endregion

            builder.SetupBaseEntity();

        }

    }
}
