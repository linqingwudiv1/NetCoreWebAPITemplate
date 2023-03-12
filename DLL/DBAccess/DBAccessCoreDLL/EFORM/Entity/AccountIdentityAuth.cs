using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace DBAccessCoreDLL.EFORM.Entity
{
    /// <summary>
    /// 用户第三方验证扩展
    /// </summary>
    public class AccountIdentityAuth : BaseEntity
    {

        [Key]
        public Int64 Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int64 AccountId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Account account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int IdentityType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Identifier{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public bool bVerifier { get; set; }

        /// <summary>
        /// 通行凭证
        /// </summary>
        public string AccessToken { get; set; }
    }

    /// <summary>
    /// Entity Config
    /// </summary>
    public class AccountIdentityAuthEFConfig : IEntityTypeConfiguration<AccountIdentityAuth>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<AccountIdentityAuth> builder)
        {
            #region 水平拆分处理处

            EntityTypeBuilder<AccountIdentityAuth> tableBuilder = builder.ToSnakeCaseTable();

            tableBuilder.HasOne<Account>(p => p.account)
                        .WithMany(c => c.AccountIdentityAuths)
                        .HasForeignKey(c => c.AccountId);



            tableBuilder.Property(x => x.bVerifier).IsRequired(true).HasDefaultValue(false);


            tableBuilder.HasIndex(x => new { x.IdentityType, x.AccountId }) .IsUnique(true);

            #endregion

            builder.SetupBaseEntity();
        }
    }
}
