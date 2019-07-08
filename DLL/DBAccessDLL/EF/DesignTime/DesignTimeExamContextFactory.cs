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
        public ExamContext CreateDbContext(string[] args)
        {
            string connectionString = "Data Source=.LocalDB/sqliteTestDB.db";
            var builder = new DbContextOptionsBuilder<ExamContext>();
            builder.UseSqlite(connectionString);
            return new ExamContext(builder.Options, connectionString);
        }
    }
}
