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
    public class ForumModuleAccesser : IForumModuleAccesser
    {
        private CoreContextDIP m_db;
        CoreContextDIP IForumModuleAccesser.db
        { 
            get => m_db; 
            set => m_db = value; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_db"></param>
        public ForumModuleAccesser(CoreContextDIP _db) 
        {
            this.m_db = _db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiy"></param>
        /// <returns></returns>
        public int Add(ForumModule newEntiy)
        {
            this.m_db.ForumModules.Add(newEntiy);
            return this.m_db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiys"></param>
        /// <returns></returns>
        public int Add(IList<ForumModule> newEntiys)
        {
            this.m_db.ForumModules.AddRange(newEntiys);
            return this.m_db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Delete(long key)
        {
            var module = this.Get(key);
            this.m_db.ForumModules. Remove(module);
            return this.m_db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public int Delete(IList<long> keys)
        {
            var modules = this.Get(keys);
            this.m_db.ForumModules.RemoveRange(modules);
            return this.m_db.SaveChanges();
        }

        public ForumModule Get(long key)
        {
            var moudule = this.m_db.ForumModules.Find(key);
            return moudule;
        }

        public IList<ForumModule> Get(IList<long> keys)
        {
            return (from x in this.m_db.ForumModules where keys.Contains(x.Id) select x).ToList();
        }

        public int Update(ForumModule modifyEntiy)
        {
            this.m_db.ForumModules.Update(modifyEntiy);
            return this.m_db.SaveChanges();
        }

        public int Update(IList<ForumModule> modifyEntiys)
        {
            this.m_db.ForumModules.UpdateRange(modifyEntiys);
            return this.m_db.SaveChanges();
        }
    }
}
