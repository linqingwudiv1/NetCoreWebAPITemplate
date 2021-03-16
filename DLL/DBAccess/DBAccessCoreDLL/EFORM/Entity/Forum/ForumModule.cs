using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DBAccessCoreDLL.EFORM.Entity.Forum
{
    /// <summary>
    /// 
    /// </summary>
    public class ForumModule : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string ModuleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<ForumTopic> Topics { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class ForumModuleEFConfig : IEntityTypeConfiguration<ForumModule>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ForumModule> builder)
        {
            var tableBuilder = builder.ToTable("ForumModule");

            //tableBuilder.HasMany(x => x.Topics).WithOne(c => c.Module);

            builder.SetupBaseEntity();
        }
    }
}
