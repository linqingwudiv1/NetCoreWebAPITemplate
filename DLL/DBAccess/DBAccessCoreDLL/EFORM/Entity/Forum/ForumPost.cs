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
    public class ForumPost : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public long TopicId { get; set; }

        [Required]
        public virtual ForumTopic Topic { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string RichText { get; set; }

        [Required]
        public long ViewsNum { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long ReplyNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<ForumReply> Replies{get;set;}

    }

    public class ForumPostEFConfig : IEntityTypeConfiguration<ForumPost>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ForumPost> builder)
        {
            var tableBuilder = builder.ToSnakeCaseTable();

            tableBuilder.HasOne(x => x.Topic).WithMany(p => p.Posts).HasForeignKey(x => x.TopicId) ;
            builder.SetupBaseEntity();
        }
    }
}
