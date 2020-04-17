using Microsoft.EntityFrameworkCore;

namespace DBAccessCoreDLL.EF.Context
{
    /// <summary>
    /// 线程池DI
    /// </summary>
    public class CoreContextDIP : CoreContext
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">多表组合</param>
        public CoreContextDIP(DbContextOptions<CoreContextDIP> options)
        {
            //base. (options as CoreContextDIP(DbContextOptions<CoreContext>);
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