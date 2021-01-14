
// #define Q_SqlServerDB
// #define Q_MySqlDB
// #define Q_SqliteDB
// #define Q_MemoryDB

using DBAccessBaseDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBAccessBaseDLL.EF.Context
{
    /// <summary>
    /// 基础
    /// </summary>
    /// <typeparam name="DBCtx"></typeparam>
    public class BaseDBContext<DBCtx> : DbContext where DBCtx : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        protected string ConnString { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ConnString"></param>
        public BaseDBContext(string _ConnString = "")
        : base()
        {
            ConnString = _ConnString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="_ConnString"></param>
        public BaseDBContext(DbContextOptions<DBCtx> options, string _ConnString = "")
        : base(options)
        {
            ConnString = _ConnString;
        }

        /// <summary>
        /// return -1 is DbUpdateConcurrencyException ( 乐观锁... )
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            var validationErrors = this.ChangeTracker;

            if (this.ChangeTracker != null)
            {
                List<EntityEntry> entities = this.ChangeTracker
                    .Entries()
                    .Where( x => x.State == EntityState.Modified &&
                                 x.Entity != null &&
                                 typeof(BaseEntity).IsAssignableFrom(x.Entity.GetType()) )
                    .Select(x => x)
                    .ToList();

                // Set the create/modified date as appropriate
                // Set the create/


                foreach (EntityEntry entity in entities)
                {
                    BaseEntity entityBase = entity.Entity as BaseEntity;

                    entityBase.Q_UpdateTime = DateTime.Now;
                    entityBase.Q_Version++;
                }
            }

            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException )
            {
                return -1;
            }

        }
    }
}
