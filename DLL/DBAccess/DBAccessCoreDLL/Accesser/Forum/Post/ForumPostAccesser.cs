using DBAccessBaseDLL.Accesser;
using System;
using System.Collections.Generic;
using System.Text;
using DBAccessCoreDLL.EFORM.Entity;
using DBAccessCoreDLL.EFORM.Context;
using BaseDLL.DTO;
using DBAccessCoreDLL.DTO.API.Users;
using System.Threading.Tasks;
using DBAccessCoreDLL.EFORM.Entity.Forum;
using System.Linq;

namespace DBAccessCoreDLL.Forum
{
    /// <summary>
    /// 
    /// </summary>
    public class ForumPostAccesser : IForumPostAccesser
    {
        /// <summary>
        /// DB Layer
        /// </summary>
        CoreContextDIP m_db;

        /// <summary>
        /// 
        /// </summary>
        CoreContextDIP IForumPostAccesser.db 
        { 
            get => m_db; 
            set => m_db = value; 
        }

        public ForumPostAccesser(CoreContextDIP _db)
        {
            this.m_db = _db;
        }

        public int Add(ForumPost newEntiy)
        {
            this.m_db.ForumPosts.Add(newEntiy);
            return this.m_db.SaveChanges();
        }

        public int Add(IList<ForumPost> newEntiys)
        {
            this.m_db.ForumPosts.AddRange(newEntiys);
            return this.m_db.SaveChanges();
        }

        public int Delete(long key)
        {
            var post = this.Get(key);
            this.m_db.ForumPosts.Remove(post);
            return this.m_db.SaveChanges();
        }

        public int Delete(IList<long> keys)
        {
            var posts = this.Get(keys);
            this.m_db.ForumPosts.RemoveRange(posts);
            return this.m_db.SaveChanges();
        }

        public ForumPost Get(long key)
        {
            var post = this.m_db.ForumPosts.Find(key);
            return  post;
        }

        public IList<ForumPost> Get(IList<long> keys)
        {
            var posts = (from x in m_db.ForumPosts where keys.Contains(x.Id) select x).ToList();
            return posts;
        }

        public int Update(ForumPost modifyEntiy)
        {
            this.m_db.ForumPosts. Update(modifyEntiy);
            return this.m_db.SaveChanges();
        }

        public int Update(IList<ForumPost> modifyEntiys)
        {
            this.m_db.ForumPosts.UpdateRange(modifyEntiys);
            return this.m_db.SaveChanges();
        }
    }
}
