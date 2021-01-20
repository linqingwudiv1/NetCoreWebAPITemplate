using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DBAccessCoreDLL.Accesser
{
    public class RoleAccesser : IRoleAccesser
    {
        /// <summary>
        /// 
        /// </summary>
        CoreContextDIP IRoleAccesser.db { get => db; set => db = value ; }
        
        private CoreContextDIP db;
        /// <summary>
        /// 
        /// </summary>
        public RoleAccesser(CoreContextDIP _db)
        {
            db = _db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiy"></param>
        /// <returns></returns>
        public int Add(Role newEntiy)
        {
            this.db.Roles.Add(newEntiy);

            int effectCount = db.SaveChanges();
            return effectCount;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiys"></param>
        /// <returns></returns>
        public int Add(IList<Role> newEntiys)
        {
            this.db.Roles.AddRange(newEntiys);
            int effectCount = db.SaveChanges();
            return effectCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Delete(long key)
        {
            Role temp_role = Get(key);
            db.Roles.Remove(temp_role);
            return db.SaveChanges();
            //this.db.Roles.Remove();
        }

        public int Delete(IList<long> keys)
        {
            IList<Role> temp_role = Get(keys);
            db.Roles.RemoveRange(temp_role);
            return db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Role Get(long key)
        {
            return db .Roles.Find(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public IList<Role> Get(IList<long> keys)
        {
            return db.Roles.Where(x => keys.Contains(x.Id)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifyEntiy"></param>
        /// <returns></returns>
        public int Update(Role modifyEntiy)
        {
            db.Roles.Update(modifyEntiy);
            return db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifyEntiys"></param>
        /// <returns></returns>
        public int Update(IList<Role> modifyEntiys)
        {
            db.Roles.UpdateRange(modifyEntiys);
            return db.SaveChanges();
        }

    }
}
