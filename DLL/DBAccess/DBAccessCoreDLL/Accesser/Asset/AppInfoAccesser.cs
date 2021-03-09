using DBAccessBaseDLL.Accesser;
using DBAccessCoreDLL.EFORM.Context;
using DBAccessCoreDLL.EFORM.Entity;
using DBAccessCoreDLL.Entity.Asset;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DBAccessBaseDLL.IDGenerator;

namespace DBAccessCoreDLL.Accesser.Asset
{
    /// <summary>
    /// 
    /// </summary>
    public class AppInfoAccesser : IAppInfoAccesser
    {
        /// <summary>
        /// 
        /// </summary>
        CoreContextDIP IAppInfoAccesser.db { get => db; set => db = value; }
        private CoreContextDIP db;

        /// <summary>
        /// 
        /// </summary>
        private readonly IIDGenerator IDGenerator;

        /// <summary>
        /// 
        /// </summary>
        public AppInfoAccesser( IIDGenerator _IDGenerator, CoreContextDIP _db) 
        {
            this.db = _db;
            this.IDGenerator = _IDGenerator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public AppInfo Get(long key)
        {
            var appinfo = this.db.AppInfos.Find(key);
            return appinfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public IList<AppInfo> Get(IList<long> keys)
        {
            return (from x in db.AppInfos where keys.Contains( x.Id ) select x ).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiy"></param>
        /// <returns></returns>
        public int Add(AppInfo newEntiy)
        {
            this.db.Add(newEntiy);
            return this.db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newEntiys"></param>
        /// <returns></returns>
        public int Add(IList<AppInfo> newEntiys)
        {
            this.db.Add(newEntiys);
            return this.db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Delete(long key)
        {
            var appInfo = this.Get(key);
            this.db.Remove(appInfo);
            return this.db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public int Delete(IList<long> keys)
        {
            var appInfos = Get(keys);
            this.db.RemoveRange(appInfos);
            return this.db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifyEntiy"></param>
        /// <returns></returns>
        public int Update(AppInfo modifyEntiy)
        {
            this.db.AppInfos.Update(modifyEntiy);
            return this.db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifyEntiys"></param>
        /// <returns></returns>
        public int Update(IList<AppInfo> modifyEntiys)
        {
            this.db.AppInfos.UpdateRange(modifyEntiys);
            return this.db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public IQueryable<AppInfo> GetByAppName(string appName)
        {
            var query = (from x in this.db.AppInfos  select x );

            if (!string.IsNullOrWhiteSpace(appName)) 
            {
                query = query.Where(x => x.AppName == appName);
            }

            return query;
        }
    }
}
