using DBAccessCoreDLL.EFORM.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DBAccessCoreDLL.EFORM.DesignTime
{
    /// <summary>
    /// 数据库迁移 代理映射
    /// </summary>
    public class DesignTimeExamContextFactory : IDesignTimeDbContextFactory<CoreContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public CoreContext CreateDbContext(string[] args)
        {
            string connectionString = "Username=postgres;Password=1qaz@WSX;Host=127.0.0.1;Port=5432;Database=QCoreDB;";
            DbContextOptionsBuilder<CoreContext> builder = new DbContextOptionsBuilder<CoreContext>();

            //builder.UseSqlite(connectionString);
            builder.UseNpgsql(connectionString);
            return new CoreContext(builder.Options, connectionString);
        }
    }
}
