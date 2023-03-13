using DBAccessBaseDLL.Static;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;

namespace DBAccessCoreDLL.EFORM.Context
{
    /// <summary>
    /// 用于DI线程池版本.
    /// </summary>
    public class CoreContextDIP : CoreContext
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">多表组合</param>
        public CoreContextDIP(DbContextOptions<CoreContextDIP> options)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            this.ConnString = GConnStrings.PostgreSQLCoreDBConn;
            // ConfigurationManager.ConnectionStrings["PostgreSQLCoreDB"].ConnectionString ; 
            optionsBuilder.UseNpgsql(ConnString).UseSnakeCaseNamingConvention();

            base.OnConfiguring(optionsBuilder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    static public class CoreContextStaticMethod
    {
        /// <summary>
        /// 暖机并确保DB生成
        /// </summary>
        static public void WarmupAndEnsureDB() 
        {
            try
            {
                
                string dbconn = GConnStrings.PostgreSQLCoreDBConn;

                DbContextOptions<CoreContextDIP> opt = new DbContextOptions<CoreContextDIP>();
                using (var db = new CoreContextDIP(opt))
                {
#if DEBUG
                    db.Database.EnsureCreated();
#endif
                    db.Accounts.FirstOrDefaultAsync();
                    db.Roles.FirstOrDefaultAsync();
                    db.RoutePages.FirstOrDefaultAsync();
                    db.RoutePageRoles.FirstOrDefaultAsync();
                    db.AccountRoles.FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                var log = LogManager.GetLogger("WarmupCoreDB");
                log.ErrorException("warmup db failed:", ex);
            }
        }
    }
}