using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DBAccessCoreDLL.EFORM.Entity.Forum
{
    ///
    public class ForumModule : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long ModuleName { get; set; }

        public virtual ICollection<ForumTopic> Topics { get; set; }
    }

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
