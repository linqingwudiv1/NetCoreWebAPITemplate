using DBAccessBaseDLL.Accesser;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DBAccessCoreDLL.EFORM.Entity;
using DBAccessCoreDLL.EFORM.Context;
using BaseDLL.DTO;
using DBAccessCoreDLL.DTO.API.Users;
using System.Threading.Tasks;
using DBAccessCoreDLL.EFORM.Entity.Forum;
using ServiceStack;

namespace DBAccessCoreDLL.Forum
{


    /// <summary>
    /// 
    /// </summary>
    public class  ForumReplyAccesser : IForumReplyAccesser
    {
        /// <summary>
        /// DB Layer
        /// </summary>
        CoreContextDIP m_db;

        CoreContextDIP IForumReplyAccesser.db 
        { 
            get => m_db; 
            set => m_db = value; 
        }

        public ForumReplyAccesser(CoreContextDIP _db)
        {
            this.m_db = _db;
        }

        public int Add(ForumReply newEntiy)
        {
            this.m_db.ForumReplies.Add(newEntiy);
            return this.m_db.SaveChanges();
        }

        public int Add(IList<ForumReply> newEntiys)
        {
            this.m_db.ForumReplies.AddRange(newEntiys);
            return this.m_db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Delete(long key)
        {
            var reply = this.Get(key);
            this.m_db.ForumReplies.Remove(reply);
            return this.m_db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public int Delete(IList<long> keys)
        {
            var replies = this.Get(keys);
            this.m_db.ForumReplies.RemoveRange(replies);
            return this.m_db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ForumReply Get(long key)
        {
            var reply = this.m_db.ForumReplies.Find();
            return reply; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public IList<ForumReply> Get(IList<long> keys)
        {
            var replies = (from x in this.m_db.ForumReplies where keys.Contains(x.Id) select x ).ToList();
            return replies;
        }

        public int Update(ForumReply modifyEntiy)
        {
            this.m_db.ForumReplies.Update(modifyEntiy);
            return this.m_db.SaveChanges();
        }

        public int Update(IList<ForumReply> modifyEntiys)
        {
            this.m_db.ForumReplies.UpdateRange(modifyEntiys);
            return this.m_db.SaveChanges();
        }
    }
}
