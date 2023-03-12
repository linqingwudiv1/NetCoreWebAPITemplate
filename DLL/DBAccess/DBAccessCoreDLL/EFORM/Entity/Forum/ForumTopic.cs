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
    public class ForumTopic : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long ModuleId {get;set;}

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string TopicName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual  ForumModule Module { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<ForumPost> Posts { get; set; }
    }
    public class ForumTopicEFConfig : IEntityTypeConfiguration<ForumTopic>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ForumTopic> builder)
        {
            var tableBuilder = builder.ToSnakeCaseTable();

            tableBuilder.HasOne(x => x.Module).WithMany(p => p.Topics).HasForeignKey(x => x.ModuleId);

            builder.SetupBaseEntity();
        }
    }

}
