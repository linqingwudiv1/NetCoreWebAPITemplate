
// #define Q_SqlServerDB
// #define Q_MySqlDB
// #define Q_SqliteDB
// #define Q_MemoryDB

using DBAccessDLL.EF.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;

namespace DBAccessDLL.EF.Context.Base
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
        /// 
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            var validationErrors = this.ChangeTracker;

            if (this.ChangeTracker != null)
            {
                var entities = this.ChangeTracker
                    .Entries()
                    .Where( x => x.State == EntityState.Modified &&
                                 x.Entity != null &&
                                 typeof(BaseEntity).IsAssignableFrom(x.Entity.GetType()) )
                    .Select(x => x)
                    .ToList();

                // Set the create/modified date as appropriate
                foreach (var entity in entities)
                {
                    BaseEntity entityBase = entity.Entity as BaseEntity;
                    entityBase.Qing_UpdateTime = DateTime.Now;
                    entityBase.Qing_Version++;
                }
            }
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException )
            {
                return 0;
                //return base.SaveChanges();
            }
            

        }
    }
}
