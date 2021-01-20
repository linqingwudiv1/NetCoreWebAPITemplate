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
            string connectionString = "Data Source=.LocalDB/sqliteTestDB.db";
            DbContextOptionsBuilder<CoreContext> builder = new DbContextOptionsBuilder<CoreContext>();

            builder.UseSqlite(connectionString);
            return new CoreContext(builder.Options, connectionString);
        }
    }
}
