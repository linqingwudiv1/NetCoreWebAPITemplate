using DBAccessCoreDLL.EFORM.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DBAccessCoreDLL.EFORM.DesignTime
{
    /// <summary>
    /// 数据库迁移 代理映射
    /// </summary>
    public class DesignTimeCoreContextFactory : IDesignTimeDbContextFactory<CoreContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public CoreContext CreateDbContext(string[] args)
        {
            string connectionString = "Username=postgres;Password=1qaz@WSX;Host=192.168.1.172;Port=5432;Database=QCoreDB;";
            DbContextOptionsBuilder<CoreContext> builder = new DbContextOptionsBuilder<CoreContext>();

            //builder.UseSqlite(connectionString);
            builder.UseNpgsql(connectionString);
            return new CoreContext(builder.Options, connectionString);
        }
    }
}
