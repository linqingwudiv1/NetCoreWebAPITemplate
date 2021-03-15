using DBAccessBaseDLL.Accesser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DBAccessCoreDLL.EFORM.Entity;
using DBAccessCoreDLL.EFORM.Context;
using BaseDLL.DTO;
using DBAccessCoreDLL.DTO.API.Users;
using System.Threading.Tasks;
using DBAccessCoreDLL.EFORM.Entity.Forum;

namespace DBAccessCoreDLL.Forum
{


    /// <summary>
    /// 
    /// </summary>
    public class ForumTopicAccesser : IForumTopicAccesser
    {
        /// <summary>
        /// DB Layer
        /// </summary>
        CoreContextDIP m_db;

        /// <summary>
        /// 
        /// </summary>
        CoreContextDIP IForumTopicAccesser.db 
        { 
            get => m_db ; 
            set => m_db = value; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_db"></param>
        public ForumTopicAccesser(CoreContextDIP _db)
        {
            this.m_db = _db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiy"></param>
        /// <returns></returns>
        public int Add(ForumTopic newEntiy)
        {
            this.m_db.ForumTopics.Add(newEntiy);
            return this.m_db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiys"></param>
        /// <returns></returns>
        public int Add(IList<ForumTopic> newEntiys)
        {
            this.m_db.ForumTopics.AddRange(newEntiys);
            return this.m_db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Delete(long key)
        {
            var topic = this.Get(key);
            this.m_db.ForumTopics.Remove(topic);
            return this.m_db.SaveChanges();
        }

        public int Delete(IList<long> keys)
        {
            var topics = this.Get(keys);

            this.m_db.ForumTopics.RemoveRange(topics);
            return this.m_db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ForumTopic Get(long key)
        {
            return this.m_db.ForumTopics.Find(key);
        }

        public IList<ForumTopic> Get(IList<long> keys)
        {
            var topics = (from x in this.m_db.ForumTopics where keys.Contains(x.Id) select x).ToList();
            return topics;
        }

        
        public int Update(ForumTopic modifyEntiy)
        {
            this.m_db.ForumTopics.Update(modifyEntiy) ;
            return this.m_db.SaveChanges();
        }

        public int Update(IList<ForumTopic> modifyEntiys)
        {
            this.m_db.ForumTopics.UpdateRange(modifyEntiys);
            return this.m_db.SaveChanges();
        }
    }
}
