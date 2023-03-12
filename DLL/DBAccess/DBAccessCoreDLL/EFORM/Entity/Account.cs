using Bogus;
using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DBAccessCoreDLL.EFORM.Entity
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
        /// 用户名
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Passport { get; set; }

        [Required]
        [DefaultValue(1)]
        public int AccountState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        /// <summary>
        /// 显示名称...
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string DisplayName { get; set; }

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
        public string? Email { get; set; }

        /// <summary>
        /// 性别 null 未知, 0  女, 1 男
        /// </summary>
        public int? Sex { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public string country { get; set; }

        
        /// <summary>
        /// 国际区号代码
        /// </summary>
        public string? PhoneAreaCode { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<AccountRole> AccountRoles { get; set; }


        /// <summary>
        /// 验证方式
        /// </summary>
        public virtual ICollection<AccountIdentityAuth> AccountIdentityAuths { get; set; }
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
            EntityTypeBuilder<Account> tableBuilder = builder.ToSnakeCaseTable();

            tableBuilder.HasMany<AccountRole>( p => p.AccountRoles )
                        .WithOne( c => c.account )
                        .HasForeignKey( c => c.AccountId );

            tableBuilder.HasMany<AccountIdentityAuth>(p => p.AccountIdentityAuths)
                        .WithOne(c => c.account)
                        .HasForeignKey(c => c.AccountId);

            //tableBuilder.Ha
            tableBuilder.Property(x => x.Sex)           .HasDefaultValue(null);
            tableBuilder.Property(x => x.DisplayName)   .HasDefaultValue("Account");
            tableBuilder.Property(x => x.Avatar)        .HasDefaultValue("");
            tableBuilder.Property(x => x.Introduction)  .HasDefaultValue("");
            tableBuilder.Property(x => x.AccountState)  .HasDefaultValue(1).IsRequired(true);

            tableBuilder.HasIndex(x => x.Username)                      .IsUnique(true);
            tableBuilder.HasIndex(x => x.Passport)                      .IsUnique(true);
            tableBuilder.HasIndex(x => x.Email)                         .IsUnique(true);
            tableBuilder.HasIndex(x => new { x.Phone, x.PhoneAreaCode}) .IsUnique(true);

            #endregion

            IList<Account> default_data = new List<Account>
            {
                new Account
                {
                    Id = 1,
                    DisplayName = "Admin",
                    Email = "875191946@qq.com".ToLower(),
                    PhoneAreaCode = "86",
                    Phone = "18412345678",
                    Passport = "Passport_Admin".ToLower(),
                    Username = "UserName_Admin",
                    Password = "1qaz@WSX",
                    Sex = null,
                },
                new Account
                {
                    Id = 2,
                    DisplayName = "Developer",
                    Email = "linqing@vip.qq.com".ToLower(),
                    PhoneAreaCode = "86",
                    Phone = "13712345678",
                    Passport = "Passport_Developer".ToLower(),
                    Username = "UserName_Developer",
                    Password = "1qaz@WSX",
                    Sex = null,
                },
                new Account
                {
                    Id = 3,
                    DisplayName = "Guest",
                    Email    = "aa875191946@vip.qq.com".ToLower(),
                    Passport = "Passport_Guest".ToLower(),
                    Username = "UserName_Guest",
                    Password = "1qaz@WSX",
                    Sex = null,
                }
            };

            builder.HasData(default_data);

            builder.SetupBaseEntity();
        }
    }
}
