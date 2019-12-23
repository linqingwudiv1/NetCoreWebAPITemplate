using DBAccessDLL.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DBAccessDLL.EF.DesignTime
{
    /// <summary>
    /// 数据库迁移 代理映射
    /// </summary>
    public class DesignTimeExamContextFactory : IDesignTimeDbContextFactory<ExamContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public ExamContext CreateDbContext(string[] args)
        {
            string connectionString = "Data Source=.LocalDB/sqliteTestDB.db";
            DbContextOptionsBuilder<ExamContext> builder = new DbContextOptionsBuilder<ExamContext>();

            builder.UseSqlite(connectionString);
            return new ExamContext(builder.Options, connectionString);
        }
    }
}
