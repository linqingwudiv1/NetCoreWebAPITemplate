using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DBAccessCoreDLL.EFORM.Entity.Forum
{
    /// <summary>
    /// 
    /// </summary>
    public class ForumReply : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }


        [Required]
        public string Title { get; set; }

        [Required]
        public string RichText { get; set; }

        /// <summary>
        /// 回复目标
        /// </summary>
        public Nullable<long> RespondentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ForumReply Respondent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<ForumReply> Replies{get;set;}

        [Required]
        public Nullable<long> PostId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ForumPost Post { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class ForumReplyEFConfig : IEntityTypeConfiguration<ForumReply>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ForumReply> builder)
        {
            var tableBuilder = builder.ToTable("ForumReply");

            tableBuilder.HasOne( x => x.Respondent).WithMany( x => x.Replies ).HasForeignKey(p => p.RespondentId);
            tableBuilder.HasOne(x => x.Post).WithMany(p => p.Replies).HasForeignKey(x => x.PostId);

            builder.SetupBaseEntity();
        }
    }
}
