using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;

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
            this.ConnString = ConfigurationManager.ConnectionStrings["PostgreSQLCoreDB"].ConnectionString;
            //optionsBuilder.UseSqlite(this.ConnString);
            optionsBuilder.UseNpgsql(ConnString);

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
}